using System;
using System.IO;
using System.Text;

namespace 发票
{
    /// <summary>
    /// 简易文件日志，记录到程序目录下的 Logs 文件夹
    /// </summary>
    public static class AppLogger
    {
        private static readonly string LogDir;
        private static readonly object LockObj = new object();

        static AppLogger()
        {
            LogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            try
            {
                Directory.CreateDirectory(LogDir);
            }
            catch { }
        }

        private static string LogFilePath =>
            Path.Combine(LogDir, $"app_{DateTime.Now:yyyyMMdd}.log");

        private static void Write(string level, string message)
        {
            try
            {
                string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                lock (LockObj)
                {
                    File.AppendAllText(LogFilePath, line + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch { /* 日志失败不应影响主流程 */ }
        }

        public static void LogInfo(string message) => Write("INFO", message);
        public static void LogWarn(string message) => Write("WARN", message);
        public static void LogError(string message) => Write("ERROR", message);
    }
}
