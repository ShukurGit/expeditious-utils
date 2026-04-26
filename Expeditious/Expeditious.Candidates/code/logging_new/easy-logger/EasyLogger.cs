

using System.Collections.Concurrent;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;


namespace Expeditious.Candidates
{
    public static class EasyLogger
    {
        private static readonly BlockingCollection<string> _queue = new();
        private static readonly CancellationTokenSource _cts = new();
        private static Task _worker;

        private static EasyLoggerOptions _options = new EasyLoggerOptions();

        private static string _lastLogFilePath = string.Empty;
        public static string LastLogFilePath { get { return _lastLogFilePath; } }


        static EasyLogger()
        {
            Directory.CreateDirectory(_options.LogRootDirectory);
            _worker = Task.Run(ProcessQueue);
        }


        public static void Configure(EasyLoggerOptions options)
        {
            _options = options ?? new EasyLoggerOptions();
            Directory.CreateDirectory(_options.LogRootDirectory);
        }


        // ===== PUBLIC API =====

        public static void Debug(string message, object data = null) =>
            Log(EasyLogLevel.Debug, message, null /*, data*/);

        public static void Info(string message, object data = null) =>
            Log(EasyLogLevel.Info, message, null /*, data*/);

        public static void Warning(string message, object data = null) =>
            Log(EasyLogLevel.Warning, message /*, null, data*/);

        public static void Error(string message, Exception ex = null, object data = null, bool? includeStack = null) =>
            Log(EasyLogLevel.Error, message, ex, /* data,*/ includeStack);

        public static void Critical(string message, Exception ex = null, object data = null) =>
            Log(EasyLogLevel.Critical, message, ex, /* data, */ true);

        // ===== CORE =====

        public static void Log(EasyLogLevel level, string message, Exception ex = null, /*object data = null,*/ bool? includeStack = null)
        {
            if (level < _options.MinLevel) return;

            bool include = includeStack ?? _options.IncludeStackTraceByDefault || level >= EasyLogLevel.Critical;

            string log = _options.UseJson
                ? EasyFormatJson(level, message, ex, /*data,*/ include)
                : EasyFormatPlain(level, message, ex, /*data,*/ include);

            _queue.Add(log);
        }

        // ===== FORMATTING =====

        private static string EasyFormatJson(EasyLogLevel level, string message, Exception ex, /*object data,*/ bool includeStack)
        {
            string errorId = ex != null ? Guid.CreateVersion7().ToString() : null;

            var logObject = new
            {
                timestamp = TimestampNowAz(),
                level = level.ToString(),
                message,
                //traceId = TraceContext.TraceId,
                //errorId,
                //data,
                exception = ex == null ? null : new
                {
                    message = ex.Message,
                    stackTrace = includeStack ? TrimStackTrace(ex.ToString()) : null
                }
            };

            return JsonSerializer.Serialize(logObject);
        }

        private static string EasyFormatPlain(EasyLogLevel level, string message, Exception ex, /*object data,*/ bool includeStack)
        {
            var sb = new StringBuilder();
            sb.Append($"{TimestampNowAz():O} [{level}] {message}");

            // if (TraceContext.TraceId != null) sb.Append($" | traceId: {TraceContext.TraceId}");

            // if (data != null) sb.Append($" | data: {JsonSerializer.Serialize(data)}");

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


        private static DateTime TimestampNowAz()
        {
            // get safely azeri time everywhere
            return TimeZoneInfo.ConvertTime((DateTimeOffset)DateTimeOffset.Now, OperatingSystem.IsWindows()
                    ? TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time")
                    : TimeZoneInfo.FindSystemTimeZoneById("Asia/Baku")).DateTime;
        }


        private static string GetFilePath()
        {
            string extension = _options.UseJson ? "json" : "txt";
               _lastLogFilePath = Path.Combine(_options.LogRootDirectory, _options.SpecificProjectsName, $"log{_options.SpecificProjectsName}_{TimestampNowAz():yyyy-MM-dd}.{extension}");
            return _lastLogFilePath; // Path.Combine(_options.LogDirectory, $"log_{DateTime.UtcNow:yyyy-MM-dd}.txt");
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
