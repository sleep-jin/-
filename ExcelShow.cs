using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 发票
{
    public partial class ExcelShow : Sunny.UI.UIForm
    {
        public string? _excelPath;
        private List<string> _data = new List<string>();
        private int _columnCount = 9;
        private AppConfig _appConfig = new AppConfig();
        public ExcelShow(Dictionary<string, string> dd, int length)
        {
            InitializeComponent();
            foreach (string item in dd.Values)
            {
                _data.Add(item);
            }
            _columnCount = length > _columnCount ? length + 2 : 9;
        }
        private void ExcelShow_Load(object sender, EventArgs e)
        {
            _appConfig = AppConfig.Load();
            textBox1.Text = _appConfig.ExcelStartRow.ToString();
            textBox2.Text = _appConfig.ExcelStartCol.ToString();
            if (!string.IsNullOrEmpty(_appConfig.ExcelTemplatePath) && File.Exists(_appConfig.ExcelTemplatePath))
            {
                textBox4.Text = _appConfig.ExcelTemplatePath;
                _excelPath = _appConfig.ExcelTemplatePath;
            }
            CreateGridColumns(_columnCount);
        }

        /// <summary>
        /// 窗口关闭时自动保存当前的行列设置
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (int.TryParse(textBox1.Text, out int row) && row >= 1)
            {
                _appConfig.ExcelStartRow = row;
            }
            if (int.TryParse(textBox2.Text, out int col) && col >= 1)
            {
                _appConfig.ExcelStartCol = col;
            }
            _appConfig.Save();
        }
        /// <summary>
        /// 创建行列数量
        /// </summary>
        /// <param name="with"></param>
        public void CreateGridColumns(int width)
        {
            for (int i = 0; i < width; i++)
            {
                dataGridView1.Columns.Add($"类型{i}", $"类型{i}");
            }
            for (int i = 0; i < _data.Count; i++)
            {
                string[] str = _data[i].Split('_');
                dataGridView1.Rows.Add(str);
            }
            dataGridView1.Rows.Add("");
            dataGridView1.Rows.Add("");
        }
        int _startRow = 13;      // 数据开始行（模板行）
        int _startCol = 1;       // 数据开始列
        int _templateRow = 13;   // 模板所在行（用于复制格式）
        private void ImportExcelButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_excelPath) || !File.Exists(_excelPath))
            {
                MessageBox.Show("请先选择有效的 Excel 文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_data.Count == 0)
            {
                MessageBox.Show("没有数据可导入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ExcelPackage.License.SetNonCommercialPersonal("-继续睡");
            var exfile = new FileInfo(_excelPath);
            _excelPackage = new ExcelPackage(exfile);
            _worksheet = _excelPackage.Workbook.Worksheets[0];

            _startRow = int.Parse(textBox1.Text);      // 数据插入起始行
            _startCol = int.Parse(textBox2.Text);      // 数据起始列
            _templateRow = _startRow;                    // 假设模板在 startRow 行
            InsertDataIntoWorksheet();

            // 保存用户设置
            _appConfig.ExcelStartRow = _startRow;
            _appConfig.ExcelStartCol = _startCol;
            if (!string.IsNullOrEmpty(_excelPath))
            {
                _appConfig.ExcelTemplatePath = _excelPath;
            }
            _appConfig.Save();

            MessageBox.Show("写入完成");
        }
        private ExcelWorksheet? _worksheet;
        private ExcelPackage? _excelPackage;
        /// <summary>
        /// 插入表格
        /// </summary>
        /// <param name="str"></param>
        public void InsertDataIntoWorksheet()
        {
            if (_worksheet == null || _data.Count == 0) return;

            // 从最后一行开始往前插入，这样前面的行号不会变
            for (int i = _data.Count - 1; i >= 0; i--)
            {
                int insertRow = _startRow + 1;  // 始终在模板行下方插入

                // 插入新行（推挤下面的行往下，不覆盖）
                _worksheet.InsertRow(insertRow, 1);

                // 复制模板格式
                _worksheet.Cells[_templateRow, 1, _templateRow, _worksheet.Dimension.End.Column]
                  .Copy(_worksheet.Cells[insertRow, 1, insertRow, _worksheet.Dimension.End.Column]);

                // 填入数据
                string[] parts = _data[i].Split('_');
                parts = new[] { i.ToString() }.Concat(parts).ToArray();
                for (int j = 0; j < parts.Length; j++)
                {
                    _worksheet.SetValue(insertRow, _startCol + j, parts[j]);
                }
            }

            _excelPackage?.Save();
        }
        private void SelectExcelFileButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog fileDialog = new OpenFileDialog
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
                    fileDialog.InitialDirectory = lastDir;
                }
            }

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = fileDialog.FileName;
                _excelPath = fileDialog.FileName;

                // 自动保存路径
                _appConfig.ExcelTemplatePath = fileDialog.FileName;
                _appConfig.Save();
            }
        }
    }
}
