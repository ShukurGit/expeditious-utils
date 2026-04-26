using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Expeditious.Candidates.code.logging_new
{


    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }

    public static class Logger
    {
        static public void Test_Run()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Logger.Configure(minLevel: LogLevel.Debug);

            Console.WriteLine(Logger.CurrentFilePath);

            Logger.Debug("Debug сообщение");
            Logger.Info("Приложение стартовало");

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    File.ReadAllText(@"c:\logs\uiroiweuoueou.txt");
                    throw new Exception("Тестовая ошибка");
                }
                catch (Exception ex)
                {
                    Logger.Error("Ошибка при выполнении", ex);
                }
            }

            

            Logger.Shutdown();
        }


        private static readonly BlockingCollection<string> _queue = new();
        private static CancellationTokenSource _cts = new();
        private static Task _worker;

        private static string _logDirectory = "logs";
        private static LogLevel _minLevel = LogLevel.Info;
        private static long _maxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

        private static string CurrentFilePath =>
            Path.Combine(_logDirectory, $"log_{DateTime.Now:yyyy-MM-dd}.txt");

        static Logger()
        {
            Directory.CreateDirectory(_logDirectory);
            _worker = Task.Run(ProcessQueue);
        }

        public static void Configure(string logDirectory = "c:\\logs\\", LogLevel minLevel = LogLevel.Info, long maxFileSizeBytes = 5 * 1024 * 1024)
        {
            _logDirectory = logDirectory;
            _minLevel = minLevel;
            _maxFileSizeBytes = maxFileSizeBytes;

            Directory.CreateDirectory(_logDirectory);
        }

        public static void Debug(string message) => Log(LogLevel.Debug, message);
        public static void Info(string message) => Log(LogLevel.Info, message);
        public static void Warning(string message) => Log(LogLevel.Warning, message);
        public static void Error(string message, Exception ex = null) => Log(LogLevel.Error, message, ex);
        public static void Critical(string message, Exception ex = null) => Log(LogLevel.Critical, message, ex);

        public static void Log(LogLevel level, string message, Exception ex = null)
        {
            if (level < _minLevel) return;

            var log = FormatMessage(level, message, ex);
            _queue.Add(log);
        }

        private static string FormatMessage(LogLevel level, string message, Exception ex)
        {
            var sb = new StringBuilder();
            sb.Append($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}");

            if (ex != null)
            {
                sb.AppendLine();
                sb.Append(ex.ToString());
            }

            return sb.ToString();
        }

        private static async Task ProcessQueue()
        {
            foreach (var log in _queue.GetConsumingEnumerable(_cts.Token))
            {
                try
                {
                    RotateIfNeeded();

                    await File.AppendAllTextAsync(CurrentFilePath, log + Environment.NewLine, Encoding.UTF8);
                    Console.WriteLine(log);
                }
                catch
                {
                    // избегаем падения логгера
                }
            }
        }

        private static void RotateIfNeeded()
        {
            if (!File.Exists(CurrentFilePath)) return;

            var fileInfo = new FileInfo(CurrentFilePath);
            if (fileInfo.Length < _maxFileSizeBytes) return;

            var archiveName = Path.Combine(
                _logDirectory,
                $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");

            File.Move(CurrentFilePath, archiveName);
        }

        public static void Shutdown()
        {
            _queue.CompleteAdding();
            _cts.Cancel();

            try
            {
                _worker.Wait(2000);
            }
            catch { }
        }
    }
}