using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace 发票
{
    /// <summary>
    /// 负责安全的文件导出（处理重名、非法文件名、目标目录创建）
    /// </summary>
    public class FileExportService
    {
        private static readonly char[] InvalidFileChars = Path.GetInvalidFileNameChars();

        public (int success, int failed) CopyFiles(Dictionary<string, string> sourceToName, string destDir, bool overwrite = false)
        {
            if (string.IsNullOrWhiteSpace(destDir)) throw new ArgumentException("目标目录为空", nameof(destDir));
            Directory.CreateDirectory(destDir);

            int success = 0, failed = 0;
            var usedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var kv in sourceToName)
            {
                string src = kv.Key;
                string name = SanitizeFileName(kv.Value);
                if (string.IsNullOrWhiteSpace(name)) name = Path.GetFileNameWithoutExtension(src);
                string destName = name + ".pdf";
                string destPath = Path.Combine(destDir, destName);

                // 处理重名
                int idx = 1;
                while ((!overwrite && File.Exists(destPath)) || usedNames.Contains(destName))
                {
                    destName = $"{name}({idx}).pdf";
                    destPath = Path.Combine(destDir, destName);
                    idx++;
                }

                try
                {
                    if (!File.Exists(src))
                    {
                        AppLogger.LogWarn($"导出跳过，源文件不存在: {src}");
                        failed++;
                        continue;
                    }
                    File.Copy(src, destPath, overwrite);
                    usedNames.Add(destName);
                    success++;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"复制文件失败 {src} -> {destPath}: {ex.Message}");
                    failed++;
                }
            }

            return (success, failed);
        }

        private string SanitizeFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;
            foreach (var c in InvalidFileChars)
            {
                name = name.Replace(c.ToString(), "_");
            }
            // 限制长度
            if (name.Length > 200) name = name.Substring(0, 200);
            return name;
        }
    }
}
