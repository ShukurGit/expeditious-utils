using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Expeditious.Candidates.code.logging_new.jiy
{


    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }

    public class LoggerOptions
    {
        public LogLevel MinLevel { get; set; } = LogLevel.Info;
        public bool IncludeStackTraceByDefault { get; set; } = false;
        public bool UseJson { get; set; } = true;
        public int MaxStackTraceLines { get; set; } = 15;
        public string LogDirectory { get; set; } = "logs";
    }

    public static class TraceContext
    {
        private static readonly AsyncLocal<string> _traceId = new();

        public static string TraceId
        {
            get => _traceId.Value;
            set => _traceId.Value = value;
        }
    }

    public static class Logger
    {
        private static readonly BlockingCollection<string> _queue = new();
        private static readonly CancellationTokenSource _cts = new();
        private static Task _worker;

        private static LoggerOptions _options = new LoggerOptions();

        static Logger()
        {
            Directory.CreateDirectory(_options.LogDirectory);
            _worker = Task.Run(ProcessQueue);
        }

        public static void Configure(LoggerOptions options)
        {
            _options = options ?? new LoggerOptions();
            Directory.CreateDirectory(_options.LogDirectory);
        }

        // ===== PUBLIC API =====

        public static void Debug(string message, object data = null) =>
            Log(LogLevel.Debug, message, null, data);

        public static void Info(string message, object data = null) =>
            Log(LogLevel.Info, message, null, data);

        public static void Warning(string message, object data = null) =>
            Log(LogLevel.Warning, message, null, data);

        public static void Error(string message, Exception ex = null, object data = null, bool? includeStack = null) =>
            Log(LogLevel.Error, message, ex, data, includeStack);

        public static void Critical(string message, Exception ex = null, object data = null) =>
            Log(LogLevel.Critical, message, ex, data, true);

        // ===== CORE =====

        public static void Log(LogLevel level, string message, Exception ex = null, object data = null, bool? includeStack = null)
        {
            if (level < _options.MinLevel) return;

            bool include = includeStack ??
                           _options.IncludeStackTraceByDefault ||
                           level >= LogLevel.Critical;

            string log = _options.UseJson
                ? FormatJson(level, message, ex, data, include)
                : FormatPlain(level, message, ex, data, include);

            _queue.Add(log);
        }

        // ===== FORMATTING =====

        private static string FormatJson(LogLevel level, string message, Exception ex, object data, bool includeStack)
        {
            string errorId = ex != null ? Guid.NewGuid().ToString() : null;

            var logObject = new
            {
                timestamp = DateTime.UtcNow,
                level = level.ToString(),
                message,
                traceId = TraceContext.TraceId,
                errorId,
                data,
                exception = ex == null ? null : new
                {
                    message = ex.Message,
                    stackTrace = includeStack ? TrimStackTrace(ex.ToString()) : null
                }
            };

            return JsonSerializer.Serialize(logObject);
        }

        private static string FormatPlain(LogLevel level, string message, Exception ex, object data, bool includeStack)
        {
            var sb = new StringBuilder();
            sb.Append($"{DateTime.UtcNow:O} [{level}] {message}");

            if (TraceContext.TraceId != null)
                sb.Append($" | traceId: {TraceContext.TraceId}");

            if (data != null)
                sb.Append($" | data: {JsonSerializer.Serialize(data)}");

            if (ex != null)
            {
                string errorId = Guid.NewGuid().ToString();
                sb.Append($" | errorId: {errorId}");
                sb.Append($" | exception: {ex.Message}");

                if (includeStack)
                {
                    sb.AppendLine();
                    sb.Append(TrimStackTrace(ex.ToString()));
                }
            }

            return sb.ToString();
        }

        private static string TrimStackTrace(string stack)
        {
            var lines = stack.Split(Environment.NewLine);

            if (lines.Length <= _options.MaxStackTraceLines)
                return stack;

            return string.Join(Environment.NewLine, lines.Take(_options.MaxStackTraceLines))
                   + Environment.NewLine + "... (truncated)";
        }

        // ===== BACKGROUND WRITER =====

        private static async Task ProcessQueue()
        {
            foreach (var log in _queue.GetConsumingEnumerable(_cts.Token))
            {
                try
                {
                    var filePath = GetFilePath();
                    await File.AppendAllTextAsync(filePath, log + Environment.NewLine);
                    Console.WriteLine(log);
                }
                catch
                {
                    // не падаем из-за логгера
                }
            }
        }

        private static string GetFilePath()
        {
            return Path.Combine(
                _options.LogDirectory,
                $"log_{DateTime.UtcNow:yyyy-MM-dd}.txt");
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