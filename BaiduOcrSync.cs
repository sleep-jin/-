using System.Text;
using System.Text.Json;

namespace 发票
{
    /// <summary>
    /// 同步调用百度 OCR 服务的简易客户端
    /// </summary>
    public class BaiduOcrSync
    {
        private readonly string _apiKey;
        private readonly string _secretKey;
        private string _accessToken = string.Empty;
        private DateTime _tokenExpireTime = DateTime.MinValue;
        private static readonly HttpClient _client = new HttpClient();
        private static readonly object _tokenLock = new object();
        private readonly bool _useStandardModel = false;

        public BaiduOcrSync(string apiKey, string secretKey, bool useStandardModel = false)
        {
            _apiKey = apiKey;
            _secretKey = secretKey;
            _useStandardModel = useStandardModel;
        }

        /// <summary>
        /// 传入Bitmap，返回识别字符
        /// </summary>
        public string Recognize(Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return RecognizeBytes(ms.ToArray());
            }
        }

        /// <summary>
        /// 传入图片路径，返回识别字符
        /// </summary>
        public string Recognize(string imagePath)
        {
            byte[] bytes = File.ReadAllBytes(imagePath);
            return RecognizeBytes(bytes);
        }

        /// <summary>
        /// 传入byte数组，返回识别字符
        /// </summary>
        public string RecognizeBytes(byte[] imageBytes)
        {
            string base64 = Convert.ToBase64String(imageBytes);
            return RecognizeBase64(base64);
        }

        private string RecognizeBase64(string base64Image)
        {
            // 获取有效Token
            string token = GetAccessToken();
            if (string.IsNullOrEmpty(token)) return "";
            string url = "";
            if (_useStandardModel)
            {
                url = $"https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic?access_token={token}";
            }
            else
            {
                url = $"https://aip.baidubce.com/rest/2.0/ocr/v1/accurate_basic?access_token={token}";
            }

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("image", base64Image)
            });
            var response = _client.PostAsync(url, content).GetAwaiter().GetResult();//发送请求
            string json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return ParseText(json);
        }

        private string ParseText(string json)
        {
            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                // 错误处理
                if (root.TryGetProperty("error_code", out _))
                {
                    System.Diagnostics.Debug.WriteLine($"OCR错误: {root.GetProperty("error_msg").GetString() ?? string.Empty}");
                    return "";
                }

                // 拼接字符
                var sb = new StringBuilder();
                foreach (var item in root.GetProperty("words_result").EnumerateArray())
                {
                    sb.Append(item.GetProperty("words").GetString() ?? string.Empty);
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"解析失败: {ex.Message}");
                return "";
            }
        }

        /// <summary>
        /// 获取百度Access Token（带缓存）
        /// </summary>
        private string GetAccessToken()
        {
            // 缓存有效直接返回
            if (!string.IsNullOrEmpty(_accessToken) && DateTime.Now < _tokenExpireTime)
            {
                return _accessToken;
            }

            lock (_tokenLock)
            {
                // 双重检查，避免重复获取
                if (!string.IsNullOrEmpty(_accessToken) && DateTime.Now < _tokenExpireTime)
                {
                    return _accessToken;
                }

                try
                {
                    string url = $"https://aip.baidubce.com/oauth/2.0/token?grant_type=client_credentials&client_id={_apiKey}&client_secret={_secretKey}";
                    var response = _client.GetStringAsync(url).GetAwaiter().GetResult();
                    using var doc = JsonDocument.Parse(response);
                    var root = doc.RootElement;

                    _accessToken = root.GetProperty("access_token").GetString() ?? string.Empty;
                    int expiresIn = root.GetProperty("expires_in").GetInt32();

                    // 提前1小时过期
                    _tokenExpireTime = DateTime.Now.AddSeconds(expiresIn - 3600);

                    System.Diagnostics.Debug.WriteLine($"Token获取成功，有效期{expiresIn}秒");
                    return _accessToken;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"获取Token失败: {ex.Message}");
                    return string.Empty;
                }
            }
        }
    }
}
