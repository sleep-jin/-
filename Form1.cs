using OfficeOpenXml;
using OpenCvSharp;
using OpenCvSharp.Aruco;
using OpenCvSharp.Extensions;
using Spire.Pdf.Fields;
using Spire.Pdf.Graphics;
using Sunny.UI;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using MessageBox = System.Windows.Forms.MessageBox;
using Rectangle = System.Drawing.Rectangle;

namespace 发票
{
    public partial class Form1 : UIForm
    {
        // ========== 百度 OCR 配置 ==========
        // 服务层
        private OcrService? _ocrService;
        private TemplateService _templateService;
        private readonly FileExportService _exportService;
        private string _lastApiKey = "";
        private string _lastApiSecret = "";
        private Dictionary<string, List<TemplateItem>> PDFclasstemp = new Dictionary<string, List<TemplateItem>>();
        private Dictionary<string, List<XMLTemplateItem>> XMLclasstemp = new Dictionary<string, List<XMLTemplateItem>>();
        string TemplatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");

        public Form1()
        {
            InitializeComponent();
            _exportService = new FileExportService();
            // _ocrService 将在 Start_Click 时根据 textBox3/textBox4 初始化

        }

        /// <summary>
        /// 初始化或刷新 OCR 服务（仅在 API Key 改变时重新创建）
        /// </summary>
        private void EnsureOcrService(string apiKey, string apiSecret)
        {
            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiSecret))
            {
                throw new ArgumentException("API Key 和 Secret 不能为空");
            }
            // 仅在密钥改变时重新创建实例，避免频繁创建
            if (_lastApiKey != apiKey || _lastApiSecret != apiSecret)
            {
                _ocrService = new OcrService(apiKey, apiSecret, Modlechoose.Active);
                _lastApiKey = apiKey;
                _lastApiSecret = apiSecret;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (textBox2.Text != null)
            {
                dialog.SelectedPath = textBox2.Text;
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dialog.SelectedPath;
            }

        }

        /// <summary>
        /// 打开文件夹并将其中的文件列入界面文件列表
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            PDFdata.Rows.Clear();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            try
            {
                if (textBox1.Text != null)
                {
                    dialog.SelectedPath = textBox1.Text;
                }
            }
            catch
            {
                throw;
            }
            if (DialogResult.OK == dialog.ShowDialog())
            {
                textBox1.Text = dialog.SelectedPath;
                DirectoryInfo info = new DirectoryInfo(textBox1.Text);
                FileInfo[] files = info.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (!file.Extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase) && !file.Extension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                        continue;
                    PDFdata.Rows.Add(Path.GetFileName(file.FullName), file.FullName, "1");
                }
            }
        }

        /// <summary>
        /// 从 PDF 中提取文本（未用于 UI 流程的辅助方法）
        /// </summary>
        public void Pdf(string path)
        {
            var txt = new System.Text.StringBuilder();
            using (var doc = UglyToad.PdfPig.PdfDocument.Open(path))
            {
                foreach (var page in doc.GetPages())
                {
                    foreach (var w in page.GetWords(NearestNeighbourWordExtractor.Instance))
                        txt.Append(w.Text).Append(' ');
                }
                CleanPdfText(txt.ToString());
            }
        }

        /// <summary>
        /// 清洗从 PDF 中提取的文本，去除多余空白与非法字符
        /// </summary>
        private string CleanPdfText(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            text = Regex.Replace(text, @"\s+", "");
            text = text.Replace("：", ":")
                       .Replace("（", "(")
                       .Replace("）", ")")
                       .Replace("￥", "¥");
            text = Regex.Replace(text, @"[^\u4e00-\u9fa5A-Za-z0-9¥\.\-:]", "");
            return text;
        }

        private void MakeMode_Click(object sender, EventArgs e)
        {
            if (FileClass.Text == "PDF")
            {
                if (PDFdata.Rows.Count == 0 || listBox1.SelectedIndex < 0) return;

                string? pdfPath = PDFdata.CurrentRow?.Cells[1].Value?.ToString();
                string? className = listBox1.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(pdfPath) || string.IsNullOrEmpty(className)) return;

                using var pdf = new Spire.Pdf.PdfDocument();
                pdf.LoadFromFile(pdfPath);
                Image image = pdf.SaveAsImage(0, PdfImageType.Bitmap, 600, 600);
                MakeModes modes = new MakeModes(image, className, TemplatePath);
                modes.ShowDialog();
                PDFclasstemp = _templateService.LoadPDFTempleta();//重新载入模板
            }
            if (FileClass.Text == "XML")
            {
                string? XMLpath = PDFdata.CurrentRow?.Cells[1].Value?.ToString();
                string? className = listBox1.SelectedItem?.ToString();
                MakeXMLMode xMLMode = new MakeXMLMode(XMLpath, className, TemplatePath);
                xMLMode.ShowDialog();
            }
        }

        private void 编辑分类_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(TemplatePath))
            {
                Directory.CreateDirectory(TemplatePath);
            }

            using (var form = new Form())
            {
                form.Text = "修改发票类型分类";
                form.ClientSize = new System.Drawing.Size(500, 400);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                var lblList = new UILabel { Text = "现有分类:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(100, 25) };
                var listBox = new UIListBox { Location = new System.Drawing.Point(20, 50), Size = new System.Drawing.Size(200, 280) };

                RefreshListBox(listBox);

                var btnAdd = new UIButton { Text = "添加分类", Location = new System.Drawing.Point(250, 50), Size = new System.Drawing.Size(120, 35) };
                var btnRename = new UIButton { Text = "重命名", Location = new System.Drawing.Point(250, 100), Size = new System.Drawing.Size(120, 35) };
                var btnDelete = new UIButton { Text = "删除分类", Location = new System.Drawing.Point(250, 150), Size = new System.Drawing.Size(120, 35) };
                var btnRefresh = new UIButton { Text = "刷新列表", Location = new System.Drawing.Point(250, 200), Size = new System.Drawing.Size(120, 35) };
                var btnClose = new UIButton { Text = "关闭", Location = new System.Drawing.Point(250, 300), Size = new System.Drawing.Size(120, 35), DialogResult = DialogResult.OK };

                btnAdd.Click += (s, ev) =>
                {
                    string newName = ShowInputDialog("请输入新分类名称:", "添加分类", "");
                    if (string.IsNullOrWhiteSpace(newName)) return;

                    string newDir = System.IO.Path.Combine(TemplatePath, newName);
                    if (Directory.Exists(newDir))
                    {
                        MessageBox.Show("该分类已存在！", "错误");
                        return;
                    }

                    try
                    {
                        Directory.CreateDirectory(newDir);
                        listBox.Items.Add(newName);
                        MessageBox.Show("分类添加成功！", "提示");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"添加失败: {ex.Message}", "错误");
                    }
                };

                btnRename.Click += (s, ev) =>
                {
                    string? oldName = listBox.SelectedItem?.ToString();
                    if (oldName == null)
                    {
                        MessageBox.Show("请先选择要重命名的分类！", "提示");
                        return;
                    }

                    string newName = ShowInputDialog($"将 [{oldName}] 重命名为:", "重命名分类", oldName);
                    if (string.IsNullOrWhiteSpace(newName) || newName == oldName) return;

                    string oldDir = System.IO.Path.Combine(TemplatePath, oldName);
                    string newDir = System.IO.Path.Combine(TemplatePath, newName);

                    if (Directory.Exists(newDir))
                    {
                        MessageBox.Show("目标分类已存在！", "错误");
                        return;
                    }

                    try
                    {
                        Directory.Move(oldDir, newDir);
                        string oldJson = System.IO.Path.Combine(newDir, $"{oldName}.json");
                        string newJson = System.IO.Path.Combine(newDir, $"{newName}.json");
                        if (File.Exists(oldJson))
                        {
                            File.Move(oldJson, newJson);
                        }

                        int index = listBox.SelectedIndex;
                        listBox.Items[index] = newName;
                        MessageBox.Show("重命名成功！", "提示");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"重命名失败: {ex.Message}", "错误");
                    }
                };

                btnDelete.Click += (s, ev) =>
                {
                    string? selectedName = listBox.SelectedItem?.ToString();
                    if (selectedName == null)
                    {
                        MessageBox.Show("请先选择要删除的分类！", "提示");
                        return;
                    }
                    if (MessageBox.Show($"确定删除分类 [{selectedName}] 吗？\n该分类下的所有模板将被删除！",
                        "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        return;
                    }

                    string dir = System.IO.Path.Combine(TemplatePath, selectedName);
                    try
                    {
                        Directory.Delete(dir, true);
                        listBox.Items.Remove(selectedName);
                        MessageBox.Show("分类已删除！", "提示");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"删除失败: {ex.Message}", "错误");
                    }
                };

                btnRefresh.Click += (s, ev) =>
                {
                    RefreshListBox(listBox);
                };

                form.Controls.AddRange(new Control[] { lblList, listBox, btnAdd, btnRename, btnDelete, btnRefresh, btnClose });

                if (form.ShowDialog() == DialogResult.OK)
                {
                }
                RefreshListBox(listBox1);
            }
        }
        #region
        private void RefreshListBox(UIListBox listBox)
        {
            listBox.Items.Clear();
            foreach (var folder in Directory.GetDirectories(TemplatePath))
            {
                string folderName = System.IO.Path.GetFileName(folder);
                listBox.Items.Add(folderName);
            }
        }

        private string ShowInputDialog(string prompt, string title, string defaultValue)
        {
            using (var form = new Form())
            {
                form.Text = title;
                form.ClientSize = new System.Drawing.Size(400, 150);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;

                var lbl = new Label { Text = prompt, Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(360, 25) };
                var txt = new TextBox { Text = defaultValue, Location = new System.Drawing.Point(20, 50), Size = new System.Drawing.Size(360, 25) };
                var btnOK = new Button { Text = "确定", Location = new System.Drawing.Point(100, 90), DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "取消", Location = new System.Drawing.Point(220, 90), DialogResult = DialogResult.Cancel };

                form.Controls.AddRange(new Control[] { lbl, txt, btnOK, btnCancel });

                return form.ShowDialog() == DialogResult.OK ? txt.Text : "";
            }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadKey();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            ExName.Clear(); PDFName.Clear();
            if (FileClass.SelectedItem.ToString() == "PDF")
            {
                #region
                Start.Enabled = false;
                Start.Text = "正在识别";
                Task.Run(() =>
                {
                    // 验证并初始化 OCR 服务
                    try
                    {
                        EnsureOcrService(textBox3.Text?.Trim() ?? "", textBox4.Text?.Trim() ?? "");
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show($"初始化 OCR 失败: {ex.Message}", "错误");
                        return;
                    }

                    // 读取模板
                    PDFclasstemp = _templateService.LoadPDFTempleta();
                    if (PDFclasstemp.Count == 0)
                    {
                        MessageBox.Show("未找到模板，请先配置模板分类");
                        return;
                    }

                    //读取模板（通过服务）
                    PDFclasstemp = _templateService.LoadPDFTempleta();


                    // ========== 进度条初始化 ==========
                    int totalFiles = PDFdata.Rows.Count;
                    int processedFiles = 0;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = totalFiles;
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;
                    lblProgress.Text = $"0 / {totalFiles}";
                    lblProgress.Visible = true;
                    Start.Enabled = false; // 防止重复点击
                    List<string> ErrorPaths = new List<string>();//分类错误发票
                                                                 // ==================================

                    for (int i = 0; i < PDFdata.Rows.Count; i++)
                    {
                        using var pdf = new Spire.Pdf.PdfDocument();
                        var txt = new System.Text.StringBuilder();
                        string? path = PDFdata.Rows[i].Cells[1].Value?.ToString();
                        if (string.IsNullOrEmpty(path)) continue;
                        pdf.LoadFromFile(path);
                        //文本化开始分类
                        using (var doc = UglyToad.PdfPig.PdfDocument.Open(path))
                        {
                            foreach (var page in doc.GetPages())
                            {
                                foreach (var w in page.GetWords(NearestNeighbourWordExtractor.Instance))
                                    txt.Append(w.Text).Append(' ');
                            }
                            string tt = CleanPdfText(txt.ToString());
                            //分类
                            for (int j = 0; j < listBox1.Items.Count; j++)
                            {
                                string? className = listBox1.Items[j]?.ToString();
                                if (className == null) continue;
                                if (tt.Contains(className))
                                {
                                    if (!PDFclasstemp.TryGetValue(className, out List<TemplateItem>? templates) || templates == null)
                                        continue;

                                    using var image = pdf.SaveAsImage(0, PdfImageType.Bitmap, 600, 600);//图片化
                                    List<string> strs = new List<string>();
                                    for (int k = 0; k < templates.Count; k++)
                                    {
                                        string tempImagepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\PDF", className, templates[k].ImageFile);
                                        using var srcBmp = new Bitmap(image);
                                        using var tempBmp = new Bitmap(tempImagepath);
                                        using var bitmap = GetRectangle(srcBmp, tempBmp, templates[k], testMode.Checked);
                                        // 使用服务层 OCR
                                        strs.Add(_ocrService!.Recognize(bitmap));
                                    }
                                    if (!ExcelFormat.TryGetValue(className, out string? excelFormat) || excelFormat == null)
                                        continue;
                                    if (!PDFFormat.TryGetValue(className, out string? pdfFormat) || pdfFormat == null)
                                        continue;
                                    try
                                    {
                                        //填入格式中
                                        string excelResult = string.Format(excelFormat, strs.ToArray());
                                        string pdfResult = string.Format(pdfFormat, strs.ToArray());

                                        PDFdata.Rows[i].Selected = true;//界面上显示进度
                                        ExName.Add(path, excelResult);
                                        PDFName.Add(path, pdfResult);
                                    }
                                    catch (FormatException)
                                    {
                                        MessageBox.Show($"{className}: 模板数量与格式填写的数量不相等,无法写入请正确填写");
                                    }
                                    if (ErrorPaths.Count > 0 && ErrorPaths.Last() == path)
                                    {
                                        ErrorPaths.Remove(path);
                                    }
                                    break;
                                }
                                else
                                {
                                    if (ErrorPaths.Last() != path)
                                    {
                                        ErrorPaths.Add(path);
                                    }
                                }
                            }
                        }

                        // ========== 更新进度 ==========
                        processedFiles++;
                        progressBar1.Value = processedFiles;
                        lblProgress.Text = $"{processedFiles} / {totalFiles}";
                        PDFdata.FirstDisplayedScrollingRowIndex = i; // 自动滚动到当前行
                    }
                    // ========== 识别完成，恢复状态 ==========
                    Start.Enabled = true;
                    progressBar1.Value = progressBar1.Maximum;
                    lblProgress.Text = $"完成: {processedFiles} / {totalFiles}";
                    // =======================================
                    this.Invoke(() =>
                    {
                        if (MessageBox.Show($"识别结束,分类失败{ErrorPaths.Count}个。是否移动到Error位置", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            string destpeth = textBox1.Text + "\\Error";
                            Directory.CreateDirectory(textBox1.Text + "\\Error");
                            foreach (var item in ErrorPaths)
                            {
                                File.Move(item, destpeth + "\\" + Path.GetFileName(item));
                            }
                        }
                        Start.Enabled = true;
                        Start.Text = "开始识别";
                    });
                });
                #endregion
            }
            else if (FileClass.SelectedItem.ToString() == "XML")
            {
                Start.Enabled = false;
                Start.Text = "正在识别";
                Task.Run(() =>
                {
                    for (int i = 0; i < PDFdata.Rows.Count; i++)
                    {
                        string? path = PDFdata.Rows[i].Cells[1].Value?.ToString();
                        XDocument document = XDocument.Load(path);
                        string xmlContent = File.ReadAllText(path, Encoding.UTF8);
                        for (int j = 0; j < listBox1.Items.Count; j++)
                        {
                            string? className = listBox1.Items[j]?.ToString();
                            if (className == null|| !xmlContent.Contains(className)) continue;
                            List<string> xmlformat = new List<string>();
                            if (xmlContent.Contains(className))//分类
                            {
                                List<XMLTemplateItem> xML = XMLclasstemp[className];
                                foreach (XMLTemplateItem item in xML)
                                {
                                   xmlformat.Add(ReadXPath(item.XPath, document));//读取完数据
                                }
                            }

                            if (!ExcelFormat.TryGetValue(className, out string? excelFormat) || excelFormat == null)
                                continue;
                            if (!PDFFormat.TryGetValue(className, out string? pdfFormat) || pdfFormat == null)
                                continue;

                            string excelResult = string.Format(excelFormat, xmlformat.ToArray());
                            string pdfResult = string.Format(pdfFormat, xmlformat.ToArray());
                            ExName.Add(path, excelResult);
                            PDFName.Add(path, pdfResult);
                        }
                    }
                    Start.Enabled = true;
                    Start.Text = "开始识别";
                });
            }

        }
        Dictionary<string, string> ExName = new Dictionary<string, string>();
        Dictionary<string, string> PDFName = new Dictionary<string, string>();
        #region 运行OCR
        // 模板加载已移至 TemplateService
        /// <summary>
        /// 进行模板匹配 创建ROI后进行OCR识别
        /// </summary>
        public Bitmap GetRectangle(Bitmap image, Bitmap temp, TemplateItem template, bool test)
        {
            string[] strs = template.ROI.Split(',');
            if (strs.Length != 4)
                throw new FormatException($"ROI 格式错误: {template.ROI}，期望格式为 X,Y,Width,Height");
            OpenCvSharp.Point point = OpterCV.RetancROI(image, temp, test);
            Rectangle rectangle = new Rectangle(
                point.X + int.Parse(strs[0]),
                point.Y + int.Parse(strs[1]),
                int.Parse(strs[2]),
                int.Parse(strs[3]));
            return OpterCV.CropImage(image, rectangle);
        }
        /// <summary>
        /// 上传OCR
        /// </summary>
        public String RunOCR(Bitmap image)
        {
            // 旧方法保留但不再创建新实例，使用服务层 OCR
            return _ocrService.Recognize(image);
        }
        #endregion

        private void setTXTFomat_Click(object sender, EventArgs e)
        {
            if (FileClass.Text == "PDF")
            {
                string? key = listBox1.SelectedItem?.ToString();
                if (key == null) return;
                ExcelFormat[key] = txtFomatbox.Text;
                PDFFormat[key] = PDFfomat.Text;
                RefreshFormat();
            }
            else if (FileClass.Text == "XML")
            {
                string? key = listBox1.SelectedItem?.ToString();
                if (key == null) return;
                ExcelFormat[key] = txtFomatbox.Text;
                PDFFormat[key] = PDFfomat.Text;
                RefreshFormat();
            }
        }
        #region
        Dictionary<string, string> ExcelFormat = new Dictionary<string, string>();
        Dictionary<string, string> PDFFormat = new Dictionary<string, string>();

        string excelConfigPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\PDF", "Config.txt");
        string pdfConfigPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\PDF", "ConfigPDF.txt");
        /// <summary>
        /// 刷新并保存 Excel 和 PDF 输出格式配置
        /// </summary>
        private void RefreshFormat()
        {
            // 确保 Templates 目录存在（一次操作）
            string templatesDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\PDF");
            Directory.CreateDirectory(templatesDir);

            // 配置文件路径

            // 构建配置内容（Excel 格式与 PDF 文件名格式）
            var excelLines = new List<string>(listBox1.Items.Count);
            var pdfLines = new List<string>(listBox1.Items.Count);

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string key = listBox1.Items[i].ToString();
                string excelFormat = ExcelFormat.ContainsKey(key) ? ExcelFormat[key] : string.Empty;
                string pdfFormat = PDFFormat.ContainsKey(key) ? PDFFormat[key] : string.Empty;

                excelLines.Add($"{key}:{excelFormat}");
                pdfLines.Add($"{key}:{pdfFormat}");
            }

            // WriteAllLines 自动创建或覆盖文件，无需手动删除
            try
            {
                File.WriteAllLines(excelConfigPath, excelLines);
                File.WriteAllLines(pdfConfigPath, pdfLines);
                System.Diagnostics.Debug.WriteLine("配置文件已保存");
            }
            catch (IOException ex)
            {
                MessageBox.Show($"保存配置文件失败: {ex.Message}", "错误");
            }
        }
        #endregion

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FileClass.Text == "PDF")
            {
                showFomat.Text = null;
                string key = listBox1.SelectedItem?.ToString();
                if (key == null) return;

                txtFomatbox.Text = ExcelFormat.TryGetValue(key, out var excelFmt) ? excelFmt : "";
                PDFfomat.Text = PDFFormat.TryGetValue(key, out var pdfFmt) ? pdfFmt : "";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(key + ": ");

                if (PDFclasstemp.TryGetValue(key, out List<TemplateItem> items))
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        sb.AppendLine($"{i} : {items[i].ClassName}");
                    }
                }
                showFomat.Text = sb.ToString();
            }
            else if (FileClass.Text == "XML")
            {
                showFomat.Text = null;
                string key = listBox1.SelectedItem?.ToString();
                if (key == null) return;
                txtFomatbox.Text = ExcelFormat.TryGetValue(key, out var excelFmt) ? excelFmt : "";
                PDFfomat.Text = PDFFormat.TryGetValue(key, out var pdfFmt) ? pdfFmt : "";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(key + ": ");
                if (XMLclasstemp.TryGetValue(key, out List<XMLTemplateItem> items))
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        sb.AppendLine($"{i} : {items[i].ClassName}");
                    }
                }
                showFomat.Text = sb.ToString();
            }
        }

        private async void outPDF_Click(object sender, EventArgs e)
        {
            string path = textBox2.Text;
            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("请选择目标目录");
                return;
            }
            if (PDFName == null || PDFName.Count == 0)
            {
                MessageBox.Show("没有数据可导出");
                return;
            }
            if (FileClass.Text == "PDF")
            {
                try
                {
                    outPDF.Enabled = false;
                    // 在后台执行复制，避免阻塞 UI
                    var result = await Task.Run(() => _exportService.CopyFiles(PDFName, path, overwrite: false));
                    MessageBox.Show($"导出完成：成功 {result.success}，失败 {result.failed}");
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"导出失败：{ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"导出过程中出现错误：{ex.Message}");
                }
                finally
                {
                    outPDF.Enabled = true;
                }
            }
            else if(FileClass.Text =="XML")
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                MessageBox.Show("请选择保存PDF的文件夹，通过相同的名称自动识别");
                dialog.ShowDialog();
                string ppath = dialog.SelectedPath;

                foreach (var item in PDFName)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item.Key) + ".pdf";
                    string succorPDFPath = Path.Combine(ppath, fileName);
                    string destPath = Path.Combine(textBox2.Text,item.Value+".pdf");
                    try
                    {
                        File.Copy(succorPDFPath, destPath, overwrite: false);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"文件 {fileName} 导出失败: {ex.Message}");
                    }
                }
            }
            MessageBox.Show("导出完成");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ExName.Count == 0)
            {
                MessageBox.Show("没有数据");
                return;
            }
            int max = 0;
            foreach (string s in ExcelFormat.Values)
            {
                max = s.Split('_').Length > max ? s.Split('_').Length : max;
            }
            ExcelShow excel = new ExcelShow(ExName, max);
            excel.ShowDialog();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                var config = new ApiConfig
                {
                    AppId = textBox3.Text?.Trim() ?? "",
                    SecretKey = textBox4.Text?.Trim() ?? ""
                };
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(config, options);
                File.WriteAllText("Api.json", json);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3_TextChanged(sender, e);
            }
            else
            {
                if (File.Exists("Api.json"))
                {
                    File.Delete("Api.json");
                }
            }
        }
        private void LoadTemplate_Click(object sender, EventArgs e)
        {
            if (FileClass.Text == "PDF")
            {
                TemplatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\PDF", ModeName.Text);
                RefreshListBox(listBox1);

                excelConfigPath = System.IO.Path.Combine(TemplatePath, "Config.txt");
                pdfConfigPath = System.IO.Path.Combine(TemplatePath, "ConfigPDF.txt");
                LoadCongfig();

                _templateService = new TemplateService(TemplatePath);
                PDFclasstemp = _templateService.LoadPDFTempleta();
            }
            if (FileClass.Text == "XML")
            {
                TemplatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\XML", ModeName.Text);
                RefreshListBox(listBox1);

                excelConfigPath = System.IO.Path.Combine(TemplatePath, "Config.txt");
                pdfConfigPath = System.IO.Path.Combine(TemplatePath, "ConfigPDF.txt");

                LoadCongfig();

                _templateService = new TemplateService(TemplatePath);
                XMLclasstemp = _templateService.LoadXMLTrmoleta();
            }

        }
        public void LoadCongfig()
        {
            if (File.Exists(excelConfigPath))
            {
                ExcelFormat.Clear();
                string[] stringps = File.ReadAllLines(excelConfigPath);

                for (int i = 0; i < listBox1.Items.Count; i++)//读取表格格式
                {
                    try
                    {
                        string? itemText = listBox1.Items[i]?.ToString();
                        if (itemText == null) continue;
                        string? line = stringps.FirstOrDefault(s => s.Contains(itemText));
                        if (line == null) throw new InvalidOperationException();
                        string[] str = line.Split(':');
                        ExcelFormat.Add(str[0], str[1]);
                    }
                    catch
                    {
                        string? itemText = listBox1.Items[i]?.ToString();
                        if (itemText != null)
                            ExcelFormat.Add(itemText, " ");
                    }
                }

                PDFFormat.Clear();
                string[] ss = File.ReadAllLines(pdfConfigPath);
                for (int i = 0; i < listBox1.Items.Count; i++)//读取Pdf格式
                {
                    try
                    {
                        string? itemText = listBox1.Items[i]?.ToString();
                        if (itemText == null) continue;
                        string? line = ss.FirstOrDefault(s => s.Contains(itemText));
                        if (line == null) throw new InvalidOperationException();
                        string[] str = line.Split(':');
                        PDFFormat.Add(str[0], str[1]);
                    }
                    catch
                    {
                        string? itemText = listBox1.Items[i]?.ToString();
                        if (itemText != null)
                            PDFFormat.Add(itemText, "");
                    }
                }
            }
        }

        public void LoadKey()
        {
            if (!File.Exists("Api.json"))
            {
                checkBox1.Checked = false;
                return;
            }

            string json = File.ReadAllText("Api.json");
            var config = JsonSerializer.Deserialize<ApiConfig>(json);

            if (config != null)
            {
                textBox3.Text = config.AppId;
                textBox4.Text = config.SecretKey;
            }
        }

        private void FileClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModeName.Items.Clear();
            string pa = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", FileClass.Text);
            DirectoryInfo directory = new DirectoryInfo(pa);
            DirectoryInfo[] infos = directory.GetDirectories();
            foreach (DirectoryInfo info in infos)
            {
                ModeName.Items.Add(info.Name);
            }

        }
        /// <summary>
        /// 用 XPath 读取节点值（支持元素和属性）
        /// </summary>
        private string ReadXPath(string xpath, XDocument doc)
        {
            try
            {
                if (xpath.Contains("/@"))
                {
                    var result = doc.XPathEvaluate(xpath);
                    if (result is IEnumerable<object> enumerable)
                    {
                        var first = enumerable.FirstOrDefault();
                        return first?.ToString() ?? string.Empty;
                    }
                    return result?.ToString() ?? string.Empty;
                }
                else
                {
                    var element = doc.XPathSelectElement(xpath);
                    return element?.Value ?? string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        private void ClassMode_Click(object sender, EventArgs e)
        {

        }
    }
    public class ApiConfig
    {
        public string AppId { get; set; } = "";
        public string SecretKey { get; set; } = "";
    }
}
