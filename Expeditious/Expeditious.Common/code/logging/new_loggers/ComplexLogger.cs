
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;


namespace Expeditious.Common
{

    // !!!!!!!!!!!!!!!!!!!
    // НЕ ЗАБЫВАЙ В КОНЦЕ !!!! ->    EasyLogger.Shutdown();

    public static class ComplexLogger
    {
        private static readonly BlockingCollection<string> _queue = new();
        private static readonly CancellationTokenSource _cts = new();
        private static Task _worker;

        public static CommonLoggerOptions _options = new CommonLoggerOptions();

        private static string _lastLogFilePath = string.Empty;
        public static string LastLogFilePath { get { return _lastLogFilePath; } }


        static ComplexLogger()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Directory.CreateDirectory(_options.SpecificProjectFolder);
            _worker = Task.Run(ProcessQueue);
        }


        public static void Configure(CommonLoggerOptions options)
        {
            _options = options ?? new CommonLoggerOptions();
            Directory.CreateDirectory(_options.SpecificProjectFolder);
        }

        private static bool IncludeStackInfo(CommonLogLevel level, bool? includeStack = null)
        {
            return includeStack ?? _options.IncludeStackTraceByDefault || level >= CommonLogLevel.Critical;
        }


        

        public static void Debug(string message, object? data = null, string? errorId = null, string? traceId = null, string? userId = null) =>
            Log(CommonLogLevel.Debug, message, ex: null, data, includeStack: false, errorId, traceId, userId);

        public static void Info(string message, object? data = null, string? errorId = null, string? traceId = null, string? userId = null) =>
            Log(CommonLogLevel.Info, message, ex: null, data, includeStack: false, errorId, traceId, userId);

        public static void Warning(string message, object? data = null, string? errorId = null, string? traceId = null, string? userId = null) =>
            Log(CommonLogLevel.Warning, message, ex: null, data, includeStack: false, errorId, traceId, userId);

        public static void Error(string message, Exception? ex = null, object? data = null, bool includeStack = false,
            string? errorId = null, string? traceId = null, string? userId = null) =>
            Log(CommonLogLevel.Error, message, ex, data, includeStack, errorId, traceId, userId);

        public static void Critical(string message, Exception? ex = null, object? data = null, 
            string? errorId = null, string? traceId = null, string? userId = null) =>
            Log(CommonLogLevel.Critical, message, ex, data, includeStack: true, errorId, traceId, userId);

        


        public static void Log(CommonLogLevel level, string message, Exception? ex, object? data, bool includeStack,
             string? errorId = null, string? traceId = null, string? userId = null)
        {
            if (level < _options.MinLevel) return;

            bool include = IncludeStackInfo(level, includeStack);

            errorId = ex != null && !string.IsNullOrWhiteSpace(errorId) ? errorId.Trim() : Guid.CreateVersion7().ToString();
            // traceId = TraceContext.TraceId;
            traceId = string.IsNullOrWhiteSpace(traceId) ? "" : traceId.Trim();
            userId = string.IsNullOrWhiteSpace(userId) ? "" : userId.Trim();

            string log = _options.UseJson
                ? ComplexFormatJson(level, message, ex, data, include, errorId, traceId, userId)
                : ComplexFormatPlain(level, message, ex, data, include, errorId, traceId, userId);

            _queue.Add(log);
        }




        private static string ComplexFormatJson(CommonLogLevel level, string message, Exception? ex, object? data, bool includeStack,
             string? errorId = null, string? traceId = null, string? userId = null)
        {
            var logObject = new
            {
                timestamp = CommonLogHelper.TimestampEverywhereAzeStr(),
                level = level.ToString(),
                message,
                traceId, // = TraceContext.TraceId,
                errorId,
                data,
                exception = ex == null ? null : new
                {
                    message = ex.Message,
                    stackTrace = includeStack ? CommonLogHelper.TrimStackTrace(ex.ToString(), _options.MaxStackTraceLines) : null
                }
            };

            return JsonSerializer.Serialize(logObject);
        }




        private static string ComplexFormatPlain(CommonLogLevel level, string message, Exception? ex, object? data, bool includeStack,
             string? errorId = null, string? traceId = null, string? userId = null)
        {
            StringBuilder sb = new();
            sb.Append($"{CommonLogHelper.TimestampEverywhereAzeStr():O} | {level} | {message}");

            if (data != null) sb.Append($" | data: {JsonSerializer.Serialize(data)}");
            if (!string.IsNullOrWhiteSpace(traceId)) sb.Append($" | traceId: {traceId}");
            if (!string.IsNullOrWhiteSpace(traceId)) sb.Append($" | userId: {userId}");

            if (ex != null)
            {
                sb.Append($" | errorId: {errorId}");
                sb.Append($" | exception: {ex.Message}");

                if (includeStack)
                {
                    sb.AppendLine();
                    sb.Append(CommonLogHelper.TrimStackTrace(ex.ToString(), _options.MaxStackTraceLines));
                }
            }

            return sb.ToString();
        }



        private static async Task ProcessQueue()
        {
            foreach (var log in _queue.GetConsumingEnumerable(_cts.Token))
            {
                try
                {
                    var filePath = CommonLogHelper.GetFilePath(_options);
                    await File.AppendAllTextAsync(filePath, log + Environment.NewLine);
                    Console.WriteLine(log);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("LOGGER ERROR: " + ex);
                }
            }
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
