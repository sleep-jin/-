using System.Text.Json;

namespace 发票
{
    /// <summary>
    /// 应用程序配置管理，自动保存 API 密钥等用户设置
    /// </summary>
    public class AppConfig
    {
        public string ApiKey { get; set; } = "";
        public string SecretKey { get; set; } = "";
        public int ExcelStartRow { get; set; } = 13;
        public int ExcelStartCol { get; set; } = 1;
        public string ExcelTemplatePath { get; set; } = "";

        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appconfig.json");

        public static AppConfig Load()
        {
            if (!File.Exists(ConfigPath)) return new AppConfig();
            try
            {
                string json = File.ReadAllText(ConfigPath);
                return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
            }
            catch
            {
                return new AppConfig();
            }
        }

        public void Save()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(ConfigPath, JsonSerializer.Serialize(this, options));
            }
            catch { }
        }
    }
}
