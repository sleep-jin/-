using OpenCvSharp;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace 发票
{
    public partial class MakeModes : UIForm
    {
        private ZoomImageBox _zoomImageBox = null!;
        private List<TemplateInfo> _templateInfos = new List<TemplateInfo>();
        string _fileName = "";
        string _templatePath = "";
        private enum SelectMode
        {
            None,
            SelectTemplate,
            SelectROI
        }
        private SelectMode _currentSelectMode = SelectMode.None;
        private TemplateInfo? _currentTemplate;

        public class TemplateInfo
        {
            public Image? Image { get; set; }
            public string ClassName { get; set; } = string.Empty;
            public string ROI { get; set; } = string.Empty;
            public Rectangle TemplateRect { get; set; }
            public Image? TemplateImage { get; set; }
        }

        public MakeModes(Image image, string className,string templatePath)
        {
            _fileName = className;
            _templatePath = templatePath;
            InitializeComponent();

            if (groupBox1 == null)
            {
                MessageBox.Show("groupBox1 未找到！");
                return;
            }

            _zoomImageBox = new ZoomImageBox
            {
                Dock = DockStyle.Fill
            };

            _zoomImageBox.SelectionCompleted += ZoomBox_SelectionCompleted;

            groupBox1.Controls.Add(_zoomImageBox);
            groupBox1.PerformLayout();

            this.Load += (s, e) => _zoomImageBox.SetImage(image);
            if (this.IsHandleCreated)
                _zoomImageBox.SetImage(image);
        }

        private void ZoomBox_SelectionCompleted(object? sender, EventArgs e)
        {
            Rectangle imgRect = _zoomImageBox.SelectedImageRect;

            switch (_currentSelectMode)
            {
                case SelectMode.SelectTemplate:
                    HandleTemplateSelection(imgRect);
                    break;
                case SelectMode.SelectROI:
                    HandleRoiSelection(imgRect);
                    break;
            }

            _zoomImageBox.ClearSelection();
        }

        private void HandleTemplateSelection(Rectangle templateRect)
        {
            if (templateRect.Width < 10 || templateRect.Height < 10)
            {
                MessageBox.Show("模板区域太小，请重新选择", "提示");
                return;
            }

            var result = MessageBox.Show(
                $"模板区域: {templateRect.X},{templateRect.Y}, {templateRect.Width},{templateRect.Height}\n\n" +
                "确认后将继续选择 ROI 区域",
                "确认模板区域",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                _currentSelectMode = SelectMode.None;
                return;
            }

            // 截取模板图像（用于表格显示）
            if (_zoomImageBox.Image == null) return;
            Image? templateImage = CropImage(_zoomImageBox.Image, templateRect);
            if (templateImage == null) return;

            _currentTemplate = new TemplateInfo
            {
                TemplateRect = templateRect,
                TemplateImage = templateImage,
                Image = templateImage,  // 表格显示用：第一次框选的图像
                ClassName = "默认分类"
            };

            _currentSelectMode = SelectMode.SelectROI;
            _zoomImageBox.SetHighlightRect(templateRect);

            MessageBox.Show(
                "请框选 ROI 区域\n\n" +
                "ROI 将计算为相对于模板左上角的偏移",
                "选择 ROI",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void HandleRoiSelection(Rectangle roiRect)
        {
            if (roiRect.Width < 5 || roiRect.Height < 5)
            {
                MessageBox.Show("ROI 区域太小，请重新选择", "提示");
                return;
            }

            // 计算相对坐标
            int relativeX = roiRect.X - _currentTemplate.TemplateRect.X;
            int relativeY = roiRect.Y - _currentTemplate.TemplateRect.Y;

            Rectangle finalROI = new Rectangle(relativeX, relativeY, roiRect.Width, roiRect.Height);

            var result = MessageBox.Show(
                $"模板区域: {_currentTemplate.TemplateRect.X},{_currentTemplate.TemplateRect.Y}, " +
                $"{_currentTemplate.TemplateRect.Width},{_currentTemplate.TemplateRect.Height}\n\n" +
                $"ROI 原始: {roiRect.X},{roiRect.Y}, {roiRect.Width},{roiRect.Height}\n" +
                $"相对偏移: {relativeX},{relativeY}\n\n" +
                $"最终填入 ROI: {finalROI.X},{finalROI.Y}, {finalROI.Width},{finalROI.Height}\n\n" +
                "确认添加？",
                "确认 ROI",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 只设置 ROI 字符串，Image 已经在 HandleTemplateSelection 中设置好了
                _currentTemplate.ROI = $"{finalROI.X},{finalROI.Y},{finalROI.Width},{finalROI.Height}";

                _templateInfos.Add(_currentTemplate);
                AddTemplateToGrid(_currentTemplate);
            }

            _currentSelectMode = SelectMode.None;
            _currentTemplate = null;
            _zoomImageBox.ClearHighlight();
        }

        private void AddTemplateToGrid(TemplateInfo template)
        {
            int rowIndex = dataGridView1.Rows.Add();
            dataGridView1.Rows[rowIndex].Cells["Column1"].Value = template.Image;
            dataGridView1.Rows[rowIndex].Cells["classname"].Value = template.ClassName;
            dataGridView1.Rows[rowIndex].Cells["Column2"].Value = template.ROI;
            dataGridView1.Rows[rowIndex].Height = 100;
        }

        private Image? CropImage(Image? source, Rectangle rect)
        {
            if (source == null || rect.Width <= 0 || rect.Height <= 0) return null;
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(source, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            }
            return bmp;
        }

        private void AddTemplateButton_Click(object sender, EventArgs e)
        {
            if (_currentSelectMode != SelectMode.None)
            {
                MessageBox.Show("正在选择中，请先完成当前操作", "提示");
                return;
            }

            _currentSelectMode = SelectMode.SelectTemplate;

            MessageBox.Show(
                "第一步：框选模板区域\n" +
                "第二步：框选 ROI 区域（任意位置）\n\n" +
                "ROI = ROI顶点 - 模板顶点，使用ROI长宽",
                "开始添加模板",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void DeleteTemplateButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要删除的模板！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 获取选中行的索引（支持多选）
            var selectedIndices = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Index >= 0 && row.Index < _templateInfos.Count)
                    selectedIndices.Add(row.Index);
            }

            if (selectedIndices.Count == 0) return;

            // 确认删除
            string msg = selectedIndices.Count == 1
                ? "确定删除选中的模板吗？"
                : $"确定删除选中的 {selectedIndices.Count} 个模板吗？";

            var result = MessageBox.Show(msg, "确认删除",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            // 从后往前删，避免索引错乱
            selectedIndices.Sort((a, b) => b.CompareTo(a));

            foreach (int index in selectedIndices)
            {
                // 释放图片资源
                _templateInfos[index].Image?.Dispose();
                _templateInfos[index].TemplateImage?.Dispose();

                // 先删数据源，再删表格行
                _templateInfos.RemoveAt(index);
                dataGridView1.Rows.RemoveAt(index);
            }

            // 刷新显示
            _zoomImageBox?.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && _currentSelectMode != SelectMode.None)
            {
                _currentSelectMode = SelectMode.None;
                _currentTemplate = null;
                _zoomImageBox.ClearHighlight();
                MessageBox.Show("已取消选择", "提示");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            foreach (var template in _templateInfos)
            {
                template.Image?.Dispose();
                template.TemplateImage?.Dispose();
            }
            _templateInfos.Clear();
        }

        private void SaveTemplateButton_Click(object sender, EventArgs e)
        {
            if (_templateInfos.Count == 0)
            {
                MessageBox.Show("没有模板可保存！", "提示");
                return;
            }

            string baseDir = Path.Combine(_templatePath, _fileName);
            Directory.CreateDirectory(baseDir);

            var config = new TemplateConfig();

            for (int i = 0; i < _templateInfos.Count; i++)
            {
                var tpl = _templateInfos[i];

                if (tpl.TemplateImage == null)
                {
                    MessageBox.Show($"模板 {i} 的图片为空，跳过");
                    continue;
                }

                // 从表格读取分类名称
                string className = dataGridView1.Rows[i].Cells["classname"].Value?.ToString()
                    ?? tpl.ClassName
                    ?? $"template_{i}";

                // 处理非法字符
                string safeClassName = MakeValidFileName(className);
                string imgName = $"{safeClassName}.png";
                string imgPath = Path.Combine(baseDir, imgName);

                try
                {
                    // 直接覆盖保存
                    using (var clone = new Bitmap(tpl.TemplateImage))
                    {
                        clone.Save(imgPath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"保存图片失败：{imgPath}\n{ex.Message}");
                    continue;
                }

                config.Templates.Add(new TemplateItem
                {
                    ClassName = className,
                    ImageFile = imgName,
                    ROI = tpl.ROI
                });
            }

            string configPath = Path.Combine(baseDir, $"{_fileName}.json");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = null,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(config, options);
            File.WriteAllText(configPath, json, System.Text.Encoding.UTF8);
        }
        private string MakeValidFileName(string name)
        {
            if (string.IsNullOrEmpty(name)) return "未命名";

            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
            {
                name = name.Replace(c, '_');
            }

            name = name.Trim().TrimEnd('.');
            if (name.Length > 50) name = name.Substring(0, 50);

            return string.IsNullOrEmpty(name) ? "未命名" : name;
        }
        private void MakeModes_Load(object sender, EventArgs e)
        {
            string baseDir = Path.Combine(_templatePath, _fileName);
            string configPath = Path.Combine(baseDir, $"{_fileName}.json");

            if (!File.Exists(configPath))
            {
                MessageBox.Show($"未找到配置文件：{configPath}", "错误");
                return;
            }

            try
            {
                string json = File.ReadAllText(configPath, System.Text.Encoding.UTF8);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null
                };

                var config = JsonSerializer.Deserialize<TemplateConfig>(json, options);

                // 清空现有（释放旧资源）
                foreach (var t in _templateInfos)
                {
                    t.Image?.Dispose();
                    t.TemplateImage?.Dispose();
                }
                _templateInfos.Clear();
                dataGridView1.Rows.Clear();

                // 加载
                foreach (var item in config.Templates)
                {
                    string imgPath = Path.Combine(baseDir, item.ImageFile);
                    Image? img = null;

                    if (File.Exists(imgPath))
                    {
                        // 关键修复：用 new Bitmap 创建副本，不锁定文件
                        using (var temp = Image.FromFile(imgPath))
                        {
                            img = new Bitmap(temp);
                        }
                    }

                    var tpl = new TemplateInfo
                    {
                        ClassName = item.ClassName,
                        Image = img,          // 表格显示用
                        TemplateImage = img,  // 保存时用（独立的内存副本）
                        ROI = item.ROI
                    };

                    _templateInfos.Add(tpl);

                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Column1"].Value = tpl.Image;
                    dataGridView1.Rows[rowIndex].Cells["classname"].Value = tpl.ClassName;
                    dataGridView1.Rows[rowIndex].Cells["Column2"].Value = tpl.ROI;
                    dataGridView1.Rows[rowIndex].Height = 100;
                }

                MessageBox.Show($"加载成功！\n共 {_templateInfos.Count} 个模板", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载失败：{ex.Message}", "错误");
            }
        }
    }
}
