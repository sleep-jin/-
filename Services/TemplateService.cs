using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace 发票
{
    /// <summary>
    /// 负责模板加载与解析
    /// </summary>
    public class TemplateService
    {
        private readonly string _templatesDir;

        public TemplateService(string templatesDir)
        {
            _templatesDir = templatesDir;
        }

        public Dictionary<string, List<TemplateItem>> LoadPDFTempleta()
        {
            var result = new Dictionary<string, List<TemplateItem>>();
            if (!Directory.Exists(_templatesDir)) return result;

            string modenames = "";
            foreach (var folder in Directory.GetDirectories(_templatesDir))
            {
                string name = Path.GetFileName(folder);
                string jsonPath = Path.Combine(folder, $"{name}.json");
                if (!File.Exists(jsonPath)) continue;
                string json = File.ReadAllText(jsonPath);
                var root = JsonSerializer.Deserialize<TemplateConfig>(json);
                if (root?.Templates != null)
                {
                    result[name] = root.Templates;
                }
                modenames += name + "，";
            }
            MessageBox.Show($"成功加载：{result.Count}个模板:{modenames.TrimEnd('，')}");
            return result;
        }
        public Dictionary<string, List<XMLTemplateItem>> LoadXMLTrmoleta()
        {
            var result = new Dictionary<string, List<XMLTemplateItem>>();
            if (!Directory.Exists(_templatesDir)) return result;

            string modenames = "";
            foreach (var folder in Directory.GetDirectories(_templatesDir))
            {
                string name = Path.GetFileName(folder);
                string jsonPath = Path.Combine(folder, $"{name}.json");

                if (!File.Exists(jsonPath)) continue;

                try
                {
                    string json = File.ReadAllText(jsonPath);
                    var items = JsonSerializer.Deserialize<List<XMLTemplateItem>>(json);

                    if (items?.Count > 0)
                    {
                        result[name] = items;
                    }
                }
                catch (Exception ex)
                {
                    // 记录日志或根据需求处理
                    Console.WriteLine($"加载节点 {name} 失败: {ex.Message}");
                }
                modenames += name + "，";
            }
            MessageBox.Show($"成功加载：{result.Count}个模板:{modenames.TrimEnd('，')}");
            return result;
        }
        public Dictionary<string, List<TemplateItem>> LoadXMLConfigon() 
        {
            var result = new Dictionary<string, List<TemplateItem>>();
            return result;
        }
    }
    public struct XMLTemplateItem
    {
        public string ClassName { get; set; }
        public string XPath { get; set; }
    }
}
