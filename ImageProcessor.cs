using OpenCvSharp;
using OpenCvSharp.Extensions;
using Point = OpenCvSharp.Point;

namespace 发票
{
    /// <summary>
    /// OpenCV 图像处理工具类
    /// </summary>
    public static class ImageProcessor
    {
        /// <summary>
        /// 使用模板匹配定位目标区域，返回匹配位置左上角坐标
        /// </summary>
        public static Point GetTemplateMatchPoint(Bitmap mainImage, Bitmap tempImage, bool debugMode)
        {
            using Mat mainMat = BitmapToMat(mainImage);
            using Mat tempMat = BitmapToMat(tempImage);
            using Mat grayMain = new Mat();
            using Mat grayTemp = new Mat();

            Cv2.CvtColor(mainMat, grayMain, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(tempMat, grayTemp, ColorConversionCodes.BGR2GRAY);

            return TemplateMatch(grayMain, grayTemp, debugMode);
        }

        /// <summary>
        /// 执行模板匹配，返回最佳匹配位置
        /// </summary>
        private static Point TemplateMatch(Mat src, Mat template, bool debugMode)
        {
            using Mat result = new Mat();
            // 归一化相关系数匹配法（对光照变化较鲁棒）
            Cv2.MatchTemplate(src, template, result, TemplateMatchModes.CCoeffNormed);

            // 找到最佳匹配位置
            Cv2.MinMaxLoc(result, out _, out _, out _, out Point maxLoc);

            // 调试模式：显示匹配结果
            if (debugMode)
            {
                using Mat debug = src.Clone();
                Point br = new Point(maxLoc.X + template.Width, maxLoc.Y + template.Height);
                debug.Rectangle(maxLoc, br, new Scalar(0, 0, 255), 10);
                Cv2.NamedWindow("模板匹配调试", WindowFlags.Normal);
                Cv2.ImShow("模板匹配调试", debug);
                Cv2.WaitKey(0);
            }

            return maxLoc;
        }

        /// <summary>
        /// Bitmap 转 OpenCV Mat
        /// </summary>
        private static Mat BitmapToMat(Bitmap bitmap)
        {
            // 处理不同像素格式
            if (bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                return BitmapConverter.ToMat(bitmap);
            }

            // 其他格式先转24位RGB
            using Bitmap bmp24 = new Bitmap(bitmap.Width, bitmap.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using Graphics g = Graphics.FromImage(bmp24);
            g.DrawImage(bitmap, 0, 0);
            return BitmapConverter.ToMat(bmp24);
        }

        /// <summary>
        /// 按指定区域裁剪图像
        /// </summary>
        public static Bitmap CropImage(Bitmap source, Rectangle roi)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (roi.Width <= 0 || roi.Height <= 0)
                throw new ArgumentException("裁剪区域宽高必须大于0", nameof(roi));

            Bitmap target = new Bitmap(roi.Width, roi.Height);
            using Graphics g = Graphics.FromImage(target);
            g.DrawImage(source,
                new Rectangle(0, 0, roi.Width, roi.Height),
                roi, GraphicsUnit.Pixel);
            return target;
        }
    }
}
