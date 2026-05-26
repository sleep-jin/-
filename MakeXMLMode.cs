using Sunny.UI;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Documents;
using System.Xml.Linq;
using System.Xml.XPath;

namespace 发票
{
    public partial class MakeXMLMode : UIForm
    {
        string _xmlPath;
        string _className;
        string _dcrition;
        XDocument _doc;
        /// <summary>
        /// 初始化一个用于生成或处理 XML 的 MakeXMLMode 实例。
        /// </summary>
        /// <remarks>构造期间初始化组件，并将提供的参数保存到对应字段。</remarks>
        /// <param name="xmlPath" 文件路径>目标 XML 文件的路径。</param>
        /// <param name="className" >要生成或处理的类名。</param>
        /// <param name="dcrition">文件夹。</param>
        public MakeXMLMode(string xmlPath, string className, string dcrition)
        {
            InitializeComponent();
            _xmlPath = xmlPath;
            _className = className;
            _dcrition = dcrition;
        }

        private void MakeXMLMode_Load(object sender, EventArgs e)
        {
            _doc = XDocument.Load(_xmlPath);

            // 读取 XML 中所有路径和值
            XMLGridView.Rows.Clear();
            foreach (var element in _doc.Root.Elements())
            {
                TraverseElement(element, element.Name.LocalName);
            }

            // 加载已保存的配置
            jsonGridView.Rows.Clear();
            string path = Path.Combine(_dcrition, _className, _className+".json");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<XMLTemplateItem> items = JsonSerializer.Deserialize<List<XMLTemplateItem>>(json);

                foreach (var item in items)
                {
                    jsonGridView.Rows.Add(item.ClassName, item.XPath, ReadXPath(item.XPath));
                }
            }
        }

        /// <summary>
        /// 用 XPath 读取节点值（支持元素和属性）
        /// </summary>
        private string ReadXPath(string xpath)
        {
            try
            {
                if (xpath.Contains("/@"))
                {
                    var result = _doc.XPathEvaluate(xpath);
                    if (result is IEnumerable<object> enumerable)
                    {
                        var first = enumerable.FirstOrDefault();
                        return first?.ToString() ?? string.Empty;
                    }
                    return result?.ToString() ?? string.Empty;
                }
                else
                {
                    var element = _doc.XPathSelectElement(xpath);
                    return element?.Value ?? string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 递归遍历元素，收集所有路径和值
        /// </summary>
        private void TraverseElement(XElement element, string currentPath)
        {
            // 记录属性
            foreach (var attr in element.Attributes())
            {
                string attrPath = $"{currentPath}/@{attr.Name.LocalName}";
                XMLGridView.Rows.Add(attrPath, attr.Value);
            }

            var childElements = element.Elements().ToList();

            if (childElements.Count == 0 && !string.IsNullOrWhiteSpace(element.Value))
            {
                string value = element.Value.Trim();
                XMLGridView.Rows.Add(currentPath, value);
            }
            else
            {
                foreach (var child in childElements)
                {
                    string childPath = $"{currentPath}/{child.Name.LocalName}";
                    TraverseElement(child, childPath);
                }
            }
        }

        /// <summary>
        /// 添加选中行到配置表
        /// </summary>
        private void addPort_Click(object sender, EventArgs e)
        {
            // DataGridView 用 SelectedRows 或 CurrentRow，没有 SelectedIndex
            if (XMLGridView.SelectedRows.Count > 0)
            {
                var row = XMLGridView.SelectedRows[0];
                string path = row.Cells[0].Value?.ToString() ?? "";
                string value = row.Cells[1].Value?.ToString() ?? "";

                // 路径转 XPath：加 // 前缀，属性保持 /@
                string xpath = path.Contains("/@") ? "//" + path : "//" + path;

                jsonGridView.Rows.Add("默认类型", xpath, value);
            }
            else if (XMLGridView.CurrentRow != null)
            {
                // 如果没有整行选中，用当前行
                var row = XMLGridView.CurrentRow;
                string path = row.Cells[0].Value?.ToString() ?? "";
                string value = row.Cells[1].Value?.ToString() ?? "";
                string xpath = "//" + path;

                jsonGridView.Rows.Add("默认类型", xpath, value);
            }
        }

        /// <summary>
        /// 删除配置表选中行
        /// </summary>
        private void deletePort_Click(object sender, EventArgs e)
        {
            if (jsonGridView.SelectedRows.Count > 0)
            {
                // 从后往前删，避免索引变化
                foreach (var row in jsonGridView.SelectedRows.Cast<DataGridViewRow>().OrderByDescending(r => r.Index))
                {
                    if (!row.IsNewRow)
                    {
                        jsonGridView.Rows.Remove(row);
                    }
                }
            }
            else if (jsonGridView.CurrentRow != null && !jsonGridView.CurrentRow.IsNewRow)
            {
                jsonGridView.Rows.Remove(jsonGridView.CurrentRow);
            }
        }

        /// <summary>
        /// 保存配置到 JSON
        /// </summary>
        private void saveJson_Click(object sender, EventArgs e)
        {
            List<XMLTemplateItem> items = new List<XMLTemplateItem>();

            // 遍历所有行（排除新行）
            foreach (DataGridViewRow row in jsonGridView.Rows)
            {
                if (row.IsNewRow) continue;

                // 用 Cells[index].Value 读取单元格值
                string claName = row.Cells[0].Value?.ToString() ?? "";
                string xpath = row.Cells[1].Value?.ToString() ?? "";

                if (!string.IsNullOrWhiteSpace(claName) && !string.IsNullOrWhiteSpace(xpath))
                {
                    items.Add(new XMLTemplateItem
                    {
                        ClassName = claName,
                        XPath = xpath
                    });
                }
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = null,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(items, options);
            string filePath = Path.Combine(_dcrition, _className, _className+".json");

            // 确保目录存在
            Directory.CreateDirectory(_dcrition);
            File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);

            UIMessageBox.Show("保存成功", "提示", UIStyle.Green);
        }
    }
}
