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
        public string? exelpath;
        private List<string> _data = new List<string>();
        private int _len = 9;
        private AppConfig _appConfig = new AppConfig();
        public ExcelShow(Dictionary<string, string> dd, int length)
        {
            InitializeComponent();
            foreach (string item in dd.Values)
            {
                _data.Add(item);
            }
            _len = length > _len ? length + 2 : 9;
        }
        private void ExcelShow_Load(object sender, EventArgs e)
        {
            _appConfig = AppConfig.Load();
            textBox1.Text = _appConfig.ExcelStartRow.ToString();
            textBox2.Text = _appConfig.ExcelStartCol.ToString();
            if (!string.IsNullOrEmpty(_appConfig.ExcelTemplatePath) && File.Exists(_appConfig.ExcelTemplatePath))
            {
                textBox4.Text = _appConfig.ExcelTemplatePath;
                exelpath = _appConfig.ExcelTemplatePath;
            }
            Creation(_len);
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
        public void Creation(int width)
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
        int startRow = 13;      // 数据开始行（模板行）
        int startCol = 1;       // 数据开始列
        int templateRow = 13;   // 模板所在行（用于复制格式）
        private void InputExcel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(exelpath) || !File.Exists(exelpath))
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
            var exfile = new FileInfo(exelpath);
            package = new ExcelPackage(exfile);
            ws = package.Workbook.Worksheets[0];

            startRow = int.Parse(textBox1.Text);      // 数据插入起始行
            startCol = int.Parse(textBox2.Text);      // 数据起始列
            templateRow = startRow;                    // 假设模板在 startRow 行
            InsertData();

            // 保存用户设置
            _appConfig.ExcelStartRow = startRow;
            _appConfig.ExcelStartCol = startCol;
            if (!string.IsNullOrEmpty(exelpath))
            {
                _appConfig.ExcelTemplatePath = exelpath;
            }
            _appConfig.Save();

            MessageBox.Show("写入完成");
        }
        private ExcelWorksheet? ws;
        private ExcelPackage? package;
        /// <summary>
        /// 插入表格
        /// </summary>
        /// <param name="str"></param>
        public void InsertData()
        {
            if (ws == null || _data.Count == 0) return;

            // 从最后一行开始往前插入，这样前面的行号不会变
            for (int i = _data.Count - 1; i >= 0; i--)
            {
                int insertRow = startRow + 1;  // 始终在模板行下方插入

                // 插入新行（推挤下面的行往下，不覆盖）
                ws.InsertRow(insertRow, 1);

                // 复制模板格式
                ws.Cells[templateRow, 1, templateRow, ws.Dimension.End.Column]
                  .Copy(ws.Cells[insertRow, 1, insertRow, ws.Dimension.End.Column]);

                // 填入数据
                string[] parts = _data[i].Split('_');
                parts = new[] { i.ToString() }.Concat(parts).ToArray();
                for (int j = 0; j < parts.Length; j++)
                {
                    ws.SetValue(insertRow, startCol + j, parts[j]);
                }
            }

            package?.Save();
        }
        private void button3_Click(object sender, EventArgs e)
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
                exelpath = fileDialog.FileName;

                // 自动保存路径
                _appConfig.ExcelTemplatePath = fileDialog.FileName;
                _appConfig.Save();
            }
        }
    }
}
