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

        /// <summary>
        /// 加载 PDF 模板配置，返回加载结果和名称列表（不再弹窗打断用户）
        /// </summary>
        public Dictionary<string, List<TemplateItem>> LoadPdfTemplates()
        {
            var result = new Dictionary<string, List<TemplateItem>>();
            if (!Directory.Exists(_templatesDir))
            {
                AppLogger.LogWarn($"模板目录不存在: {_templatesDir}");
                return result;
            }

            foreach (var folder in Directory.GetDirectories(_templatesDir))
            {
                string name = Path.GetFileName(folder);
                string jsonPath = Path.Combine(folder, $"{name}.json");
                if (!File.Exists(jsonPath)) continue;
                try
                {
                    string json = File.ReadAllText(jsonPath);
                    var root = JsonSerializer.Deserialize<TemplateConfig>(json);
                    if (root?.Templates != null)
                    {
                        result[name] = root.Templates;
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"加载 PDF 模板 {name} 失败: {ex.Message}");
                }
            }

            if (result.Count > 0)
            {
                AppLogger.LogInfo($"PDF 模板加载完成，共 {result.Count} 个: {string.Join(", ", result.Keys)}");
            }
            return result;
        }

        /// <summary>
        /// 加载 XML 模板配置
        /// </summary>
        public Dictionary<string, List<XMLTemplateItem>> LoadXmlTemplates()
        {
            var result = new Dictionary<string, List<XMLTemplateItem>>();
            if (!Directory.Exists(_templatesDir))
            {
                AppLogger.LogWarn($"模板目录不存在: {_templatesDir}");
                return result;
            }

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
                    AppLogger.LogError($"加载 XML 模板 {name} 失败: {ex.Message}");
                }
            }

            if (result.Count > 0)
            {
                AppLogger.LogInfo($"XML 模板加载完成，共 {result.Count} 个: {string.Join(", ", result.Keys)}");
            }
            return result;
        }
    }
    public struct XMLTemplateItem
    {
        public string ClassName { get; set; }
        public string XPath { get; set; }
    }
}
