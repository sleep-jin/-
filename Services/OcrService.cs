using System.Drawing;

namespace 发票
{
    /// <summary>
    /// OCR 服务封装，提供带异常保护的识别接口
    /// </summary>
    public class OcrService
    {
        private readonly BaiduOcrSync _client;

        public OcrService(string apiKey, string secret, bool useStandardModel = false)
        {
            _client = new BaiduOcrSync(apiKey, secret, useStandardModel);
        }

        /// <summary>
        /// 识别图片中的文字，失败时返回空字符串并记录日志
        /// </summary>
        public string Recognize(Bitmap bitmap)
        {
            try
            {
                return _client.Recognize(bitmap);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"OCR 识别失败: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
