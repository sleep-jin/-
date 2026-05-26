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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using MessageBox = System.Windows.Forms.MessageBox;
using Rectangle = System.Drawing.Rectangle;

namespace 发票
{
    public partial class main : UIForm
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
        private CancellationTokenSource? _recognitionCts;
        private AppConfig _appConfig = new AppConfig();
        string TemplatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\PDF");

        public main()
        {
            InitializeComponent();
            _exportService = new FileExportService();
            _templateService = new TemplateService(TemplatePath);
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
                _ocrService = new OcrService(apiKey, apiSecret, !Modlechoose.Active);
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
            if (!string.IsNullOrEmpty(textBox1.Text) && Directory.Exists(textBox1.Text))
            {
                dialog.SelectedPath = textBox1.Text;
            }
            if (DialogResult.OK == dialog.ShowDialog())
            {
                textBox1.Text = dialog.SelectedPath;
                DirectoryInfo info = new DirectoryInfo(textBox1.Text);
                FileInfo[] files = info.GetFiles();

                // 根据第一个遇到的文件类型决定整体类型
                string? detectedType = null;
                foreach (FileInfo file in files)
                {
                    if (file.Extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        detectedType = "PDF";
                        break;
                    }
                    if (file.Extension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        detectedType = "XML";
                        break;
                    }
                }

                // 自动切换文件类型下拉框
                if (detectedType != null && FileClass.Text != detectedType)
                {
                    FileClass.Text = detectedType;
                    FileClass_SelectedIndexChanged(sender, e);
                }

                string targetExt = detectedType == "XML" ? ".xml" : ".pdf";
                foreach (FileInfo file in files)
                {
                    if (!file.Extension.Equals(targetExt, StringComparison.OrdinalIgnoreCase))
                        continue;
                    PDFdata.Rows.Add(Path.GetFileName(file.FullName), file.FullName, "");
                }
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
                PDFclasstemp = _templateService.LoadPdfTemplates(); // 重新载入模板
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
                        if (!Directory.Exists(Path.Combine(TemplatePath, newName)))
                            Directory.CreateDirectory(Path.Combine(TemplatePath, newName));
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
            _appConfig = AppConfig.Load();
            LoadKey();
            if (!Directory.Exists(TemplatePath))
                Directory.CreateDirectory(TemplatePath);
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\XML")))
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\XML"));
            RefreshListBox(listBox1);
            FileClass_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// 开始识别按钮点击事件，支持取消识别
        /// </summary>
        private void Start_Click(object sender, EventArgs e)
        {
            // 如果正在识别，则取消
            if (_recognitionCts != null)
            {
                _recognitionCts.Cancel();
                return;
            }
            if (ModeName.Text=="")
            {
                MessageBox.Show("请先选择分类", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _excelOutputs.Clear(); _pdfOutputs.Clear();
            _recognitionCts = new CancellationTokenSource();
            var token = _recognitionCts.Token;

            if (FileClass.SelectedItem?.ToString() == "PDF")
            {
                Start.Text = "取消识别";
                Start.Enabled = true;
                Task.Run(() => RunPdfRecognition(token), token);
            }
            else if (FileClass.SelectedItem?.ToString() == "XML")
            {
                Start.Text = "取消识别";
                Start.Enabled = true;
                Task.Run(() => RunXmlRecognition(token), token);
            }
        }

        /// <summary>
        /// PDF 识别流程（后台线程）
        /// </summary>
        private void RunPdfRecognition(CancellationToken token)
        {
            // 验证并初始化 OCR 服务
            try
            {
                EnsureOcrService(textBox3.Text?.Trim() ?? "", textBox4.Text?.Trim() ?? "");
            }
            catch (ArgumentException ex)
            {
                this.Invoke(() => MessageBox.Show($"初始化 OCR 失败: {ex.Message}", "错误"));
                ResetRecognitionState();
                return;
            }

            // 读取模板
            PDFclasstemp = _templateService.LoadPdfTemplates();
            if (PDFclasstemp.Count == 0)
            {
                this.Invoke(() => MessageBox.Show("未找到模板，请先配置模板分类", "提示"));
                ResetRecognitionState();
                return;
            }

            int totalFiles = PDFdata.Rows.Count;
            int processedFiles = 0;
            var errorPaths = new List<string>();
            bool wasCancelled = false;

            // 初始化进度条（UI线程）
            this.Invoke(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = totalFiles;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                lblProgress.Text = $"0 / {totalFiles}";
                lblProgress.Visible = true;
            });

            for (int i = 0; i < PDFdata.Rows.Count; i++)
            {
                if (token.IsCancellationRequested)
                {
                    wasCancelled = true;
                    break;
                }

                string? path = null;
                this.Invoke(() => path = PDFdata.Rows[i].Cells[1].Value?.ToString());
                if (string.IsNullOrEmpty(path)) continue;

                try
                {
                    string tt;
                    // 文本化开始分类
                    using (var doc = UglyToad.PdfPig.PdfDocument.Open(path))
                    {
                        var txt = new System.Text.StringBuilder();
                        foreach (var page in doc.GetPages())
                        {
                            foreach (var w in page.GetWords(NearestNeighbourWordExtractor.Instance))
                                txt.Append(w.Text).Append(' ');
                        }
                        tt = CleanPdfText(txt.ToString());
                    }

                    bool classified = false;
                    for (int j = 0; j < listBox1.Items.Count; j++)
                    {
                        if (token.IsCancellationRequested) break;

                        string? className = null;
                        this.Invoke(() => className = listBox1.Items[j]?.ToString());
                        if (className == null) continue;

                        if (tt.Contains(className))
                        {
                            if (!PDFclasstemp.TryGetValue(className, out List<TemplateItem>? templates) || templates == null)
                                continue;

                            using var pdf = new Spire.Pdf.PdfDocument();
                            pdf.LoadFromFile(path);
                            using var image = pdf.SaveAsImage(0, PdfImageType.Bitmap, 600, 600);
                            List<string> strs = new List<string>();

                            for (int k = 0; k < templates.Count; k++)
                            {
                                if (token.IsCancellationRequested) break;

                                string tempImagepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\PDF", className, templates[k].ImageFile);
                                using var srcBmp = new Bitmap(image);
                                using var tempBmp = new Bitmap(tempImagepath);
                                using var bitmap = GetRectangle(srcBmp, tempBmp, templates[k], testMode.Checked);
                                strs.Add(_ocrService!.Recognize(bitmap));
                            }

                            if (token.IsCancellationRequested) break;

                            if (!ExcelFormat.TryGetValue(className, out string? excelFormat) || excelFormat == null)
                                continue;
                            if (!PDFFormat.TryGetValue(className, out string? pdfFormat) || pdfFormat == null)
                                continue;

                            try
                            {
                                string excelResult = string.Format(excelFormat, strs.ToArray());
                                string pdfResult = string.Format(pdfFormat, strs.ToArray());

                                _excelOutputs.Add(path, excelResult);
                                _pdfOutputs.Add(path, pdfResult);

                                // 在界面上显示分类结果
                                this.Invoke(() =>
                                {
                                    PDFdata.Rows[i].Cells[2].Value = className;
                                    PDFdata.Rows[i].Selected = true;
                                });
                            }
                            catch (FormatException)
                            {
                                this.Invoke(() => MessageBox.Show(
                                    $"{className}: 模板数量与格式填写的数量不相等，请正确填写格式。",
                                    "格式错误", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                            }

                            // 如果之前被标记为错误，移除
                            errorPaths.Remove(path);
                            classified = true;
                            break;
                        }
                    }

                    if (!classified && !token.IsCancellationRequested)
                    {
                        if (!errorPaths.Contains(path))
                            errorPaths.Add(path);
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"识别文件 {path} 失败: {ex.Message}");
                    if (!errorPaths.Contains(path))
                        errorPaths.Add(path);
                }

                // 更新进度（UI线程）
                processedFiles++;
                int current = processedFiles;
                this.Invoke(() =>
                {
                    progressBar1.Value = Math.Min(current, progressBar1.Maximum);
                    lblProgress.Text = $"{current} / {totalFiles}";
                    PDFdata.FirstDisplayedScrollingRowIndex = Math.Min(i, PDFdata.Rows.Count - 1);
                });
            }

            // 识别完成或取消，恢复状态
            this.Invoke(() =>
            {
                if (!wasCancelled && errorPaths.Count > 0)
                {
                    if (MessageBox.Show(
                        $"识别结束，分类失败 {errorPaths.Count} 个。是否移动到 Error 文件夹？",
                        "识别完成", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string destPath = Path.Combine(textBox1.Text, "Error");
                        Directory.CreateDirectory(destPath);
                        foreach (var item in errorPaths)
                        {
                            try
                            {
                                File.Move(item, Path.Combine(destPath, Path.GetFileName(item)));
                            }
                            catch (Exception ex)
                            {
                                AppLogger.LogError($"移动文件 {item} 失败: {ex.Message}");
                            }
                        }
                    }
                }
                else if (wasCancelled)
                {
                    MessageBox.Show("识别已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });

            ResetRecognitionState();
        }

        /// <summary>
        /// XML 识别流程（后台线程）
        /// </summary>
        private void RunXmlRecognition(CancellationToken token)
        {
            int totalFiles = PDFdata.Rows.Count;
            int processedFiles = 0;

            this.Invoke(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = totalFiles;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                lblProgress.Text = $"0 / {totalFiles}";
                lblProgress.Visible = true;
            });

            for (int i = 0; i < PDFdata.Rows.Count; i++)
            {
                if (token.IsCancellationRequested) break;

                string? path = null;
                this.Invoke(() => path = PDFdata.Rows[i].Cells[1].Value?.ToString());
                if (string.IsNullOrEmpty(path)) continue;

                try
                {
                    XDocument document = XDocument.Load(path);
                    string xmlContent = File.ReadAllText(path, Encoding.UTF8);

                    for (int j = 0; j < listBox1.Items.Count; j++)
                    {
                        string? className = null;
                        this.Invoke(() => className = listBox1.Items[j]?.ToString());
                        if (className == null || !xmlContent.Contains(className)) continue;

                        List<string> xmlformat = new List<string>();
                        if (XMLclasstemp.TryGetValue(className, out List<XMLTemplateItem>? xML))
                        {
                            foreach (XMLTemplateItem item in xML)
                            {
                                xmlformat.Add(ReadXPath(item.XPath, document));
                            }
                        }

                        if (!ExcelFormat.TryGetValue(className, out string? excelFormat) || excelFormat == null)
                            continue;
                        if (!PDFFormat.TryGetValue(className, out string? pdfFormat) || pdfFormat == null)
                            continue;

                        string excelResult = string.Format(excelFormat, xmlformat.ToArray());
                        string pdfResult = string.Format(pdfFormat, xmlformat.ToArray());
                        _excelOutputs.Add(path, excelResult);
                        _pdfOutputs.Add(path, pdfResult);

                        this.Invoke(() =>
                        {
                            PDFdata.Rows[i].Cells[2].Value = className;
                        });
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"识别 XML {path} 失败: {ex.Message}");
                }

                processedFiles++;
                int current = processedFiles;
                this.Invoke(() =>
                {
                    progressBar1.Value = Math.Min(current, progressBar1.Maximum);
                    lblProgress.Text = $"{current} / {totalFiles}";
                });
            }

            ResetRecognitionState();
        }

        /// <summary>
        /// 恢复识别按钮状态
        /// </summary>
        private void ResetRecognitionState()
        {
            _recognitionCts?.Dispose();
            _recognitionCts = null;
            this.Invoke(() =>
            {
                Start.Text = "开始识别";
                Start.Enabled = true;
                lblProgress.Text = "就绪";
            });
        }
        private readonly Dictionary<string, string> _excelOutputs = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _pdfOutputs = new Dictionary<string, string>();
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
            OpenCvSharp.Point point = ImageProcessor.GetTemplateMatchPoint(image, temp, test);
            Rectangle rectangle = new Rectangle(
                point.X + int.Parse(strs[0]),
                point.Y + int.Parse(strs[1]),
                int.Parse(strs[2]),
                int.Parse(strs[3]));
            return ImageProcessor.CropImage(image, rectangle);
        }
        /// <summary>
        /// 上传OCR
        /// </summary>
        public String RunOCR(Bitmap image)
        {
            // 旧方法保留但不再创建新实例，使用服务层 OCR
            return _ocrService?.Recognize(image) ?? string.Empty;
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
                string? key = listBox1.Items[i]?.ToString();
                if (key == null) continue;
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
                showFomat.Text = string.Empty;
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
                showFomat.Text = string.Empty;
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
            if (_pdfOutputs == null || _pdfOutputs.Count == 0)
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
                    var result = await Task.Run(() => _exportService.CopyFiles(_pdfOutputs, path, overwrite: false));
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
            else if (FileClass.Text == "XML")
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                MessageBox.Show("请选择保存PDF的文件夹，通过相同的名称自动识别");
                dialog.ShowDialog();
                string ppath = dialog.SelectedPath;

                foreach (var item in _pdfOutputs)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item.Key) + ".pdf";
                    string succorPDFPath = Path.Combine(ppath, fileName);
                    string destPath = Path.Combine(textBox2.Text, item.Value + ".pdf");
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
            if (_excelOutputs.Count == 0)
            {
                MessageBox.Show("没有数据");
                return;
            }
            int max = 0;
            foreach (string s in ExcelFormat.Values)
            {
                max = s.Split('_').Length > max ? s.Split('_').Length : max;
            }
            ExcelShow excel = new ExcelShow(_excelOutputs, max);
            excel.ShowDialog();
        }

        /// <summary>
        /// 主窗体「导入表格」按钮：选择 Excel 模板并直接导入识别数据
        /// </summary>
        private void InputExcel_Click(object sender, EventArgs e)
        {
            if (_excelOutputs.Count == 0)
            {
                MessageBox.Show("没有识别数据，请先执行识别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 重新加载配置，确保使用预览表格中修改后的最新值
            _appConfig = AppConfig.Load();

            // 选择 Excel 模板文件
            using var dialog = new OpenFileDialog
            {
                Filter = "Excel 文件|*.xlsx;*.xls",
                Title = "选择 Excel 模板文件"
            };

            // 定位到上次路径的上一层，方便快速找到表格
            if (!string.IsNullOrEmpty(_appConfig.ExcelTemplatePath))
            {
                string? lastDir = Path.GetDirectoryName(_appConfig.ExcelTemplatePath);
                if (!string.IsNullOrEmpty(lastDir) && Directory.Exists(lastDir))
                {
                    dialog.InitialDirectory = lastDir;
                }
            }

            if (dialog.ShowDialog() != DialogResult.OK) return;

            string excelPath = dialog.FileName;
            int startRow = _appConfig.ExcelStartRow;
            int startCol = _appConfig.ExcelStartCol;

            // 确认弹窗，提示当前使用的行列，如需修改请去预览表格界面
            var confirm = MessageBox.Show(
                $"将使用以下参数写入 Excel：\n\n起始行：{startRow}\n起始列：{startCol}\n\n如需修改请打开「预览表格」界面调整。\n\n是否确认导入？",
                "确认导入",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("-继续睡");
                var file = new FileInfo(excelPath);
                using var package = new ExcelPackage(file);
                var ws = package.Workbook.Worksheets[0];
                if (ws == null)
                {
                    MessageBox.Show("Excel 文件中没有工作表", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var data = _excelOutputs.Values.ToList();
                int templateRow = startRow;

                // 从最后一行开始往前插入，这样前面的行号不会变
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    int insertRow = startRow + 1;

                    // 插入新行（推挤下面的行往下，不覆盖）
                    ws.InsertRow(insertRow, 1);

                    // 复制模板格式
                    if (ws.Dimension != null && templateRow >= 1 && templateRow <= ws.Dimension.End.Row)
                    {
                        ws.Cells[templateRow, 1, templateRow, ws.Dimension.End.Column]
                          .Copy(ws.Cells[insertRow, 1, insertRow, ws.Dimension.End.Column]);
                    }

                    // 填入数据
                    string[] parts = data[i].Split('_');
                    parts = new[] { i.ToString() }.Concat(parts).ToArray();
                    int maxCol = ws.Dimension?.End.Column ?? 100;
                    for (int j = 0; j < parts.Length && (startCol + j - 1) <= maxCol; j++)
                    {
                        ws.SetValue(insertRow, startCol + j, parts[j]);
                    }
                }

                package.Save();

                // 保存用户设置
                _appConfig.ExcelStartRow = startRow;
                _appConfig.ExcelStartCol = startCol;
                _appConfig.ExcelTemplatePath = excelPath;
                _appConfig.Save();

                MessageBox.Show($"导入完成，共写入 {data.Count} 条数据", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AppLogger.LogInfo($"导入 Excel 成功: {excelPath}, 数据条数: {data.Count}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"导入 Excel 失败: {ex.Message}");
                MessageBox.Show($"导入失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (ModeName.Text == "")
            {
                TemplatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", FileClass.Text);
                RefreshListBox(listBox1);
                return;
            }
            if (FileClass.Text == "PDF")
            {
                TemplatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\PDF", ModeName.Text);
                if (!Directory.Exists(TemplatePath)) return;
                RefreshListBox(listBox1);

                excelConfigPath = System.IO.Path.Combine(TemplatePath, "Config.txt");
                pdfConfigPath = System.IO.Path.Combine(TemplatePath, "ConfigPDF.txt");
                LoadConfig();

                _templateService = new TemplateService(TemplatePath);
                PDFclasstemp = _templateService.LoadPdfTemplates();
            }
            if (FileClass.Text == "XML")
            {
                TemplatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\XML", ModeName.Text);
                if (!Directory.Exists(TemplatePath)) return;
                RefreshListBox(listBox1);

                excelConfigPath = System.IO.Path.Combine(TemplatePath, "Config.txt");
                pdfConfigPath = System.IO.Path.Combine(TemplatePath, "ConfigPDF.txt");

                LoadConfig();

                _templateService = new TemplateService(TemplatePath);
                XMLclasstemp = _templateService.LoadXmlTemplates();
            }

        }
        public void LoadConfig()
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
            try
            {
                if (!Directory.Exists(pa))
                {
                    Directory.CreateDirectory(pa);
                    return;
                }
                DirectoryInfo directory = new DirectoryInfo(pa);
                DirectoryInfo[] infos = directory.GetDirectories();
                foreach (DirectoryInfo info in infos)
                {
                    ModeName.Items.Add(info.Name);
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"刷新文件类型列表失败: {ex.Message}");
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

        private void 使用说明_Click(object sender, EventArgs e)
        {
            说明 form = new 说明();
            form.Show();
        }

        private void ModeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTemplate_Click(sender, e);
        }
    }
}
