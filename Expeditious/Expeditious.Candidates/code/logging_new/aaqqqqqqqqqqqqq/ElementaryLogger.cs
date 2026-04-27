
using System.Text;
using System.Text.Json;


namespace Expeditious.Candidates.code.logging_new.aaqqqqqqqqqqqqq
{
    public static class ElementaryLogger
    {
        public static CommonLoggerOptions _options = new CommonLoggerOptions();

        private static string _lastLogFilePath = string.Empty;
        public static string LastLogFilePath { get { return _lastLogFilePath; } }


        static ElementaryLogger()
        {
            Directory.CreateDirectory(_options.SpecificProjectFolder);
        }


        public static void Configure(CommonLoggerOptions options)
        {
            _options = options ?? new CommonLoggerOptions();
        }


        private static bool IncludeStackInfo(CommonLogLevel level, bool? includeStack = null)
        { 
            return includeStack ?? _options.IncludeStackTraceByDefault || level >= CommonLogLevel.Critical;
        }



        public static void Debug(string message) => Log(CommonLogLevel.Debug, message, null);

        public static void Info(string message) => Log(CommonLogLevel.Info, message, null);

        public static void Warning(string message) => Log(CommonLogLevel.Warning, message);

        public static void Error(string message, Exception? ex = null, bool? includeStack = null) =>
            Log(CommonLogLevel.Error, message, ex, includeStack);

        public static void Critical(string message, Exception? ex = null) =>
            Log(CommonLogLevel.Critical, message, ex, true);



        public static void Log(CommonLogLevel level, string message, Exception? ex = null, bool? includeStack = null)
        {
            if (level < _options.MinLevel) return;

            bool include = IncludeStackInfo(level, includeStack);

            string log = _options.UseJson
                ? ElementaryFormatJson(level, message, ex, include)
                : ElementaryFormatPlain(level, message, ex, include);

            _lastLogFilePath = CommonLogHelper.GetFilePath(_options);
            File.AppendAllText(LastLogFilePath, log);
        }


        private static string ElementaryFormatPlain(CommonLogLevel level, string message, Exception ex, bool includeStack)
        {
            var sb = new StringBuilder();
            sb.Append($"{CommonLogHelper.TimestampEverywhereAze():O} [{level}] {message}");

            if (ex != null)
            {
                sb.Append($" | exception: {ex.Message}");

                if (includeStack)
                {
                    sb.AppendLine();
                    sb.Append(CommonLogHelper.TrimStackTrace(ex.ToString(), _options.MaxStackTraceLines));
                }
            }

            return sb.ToString();
        }


        private static string ElementaryFormatJson(CommonLogLevel level, string message, Exception ex, bool includeStack)
        {
            var logObject = new
            {
                timestamp = CommonLogHelper.TimestampEverywhereAze(),
                level = level.ToString(),
                message,
                exception = ex == null ? null : new
                {
                    message = ex.Message,
                    stackTrace = includeStack ? CommonLogHelper.TrimStackTrace(ex.ToString(), _options.MaxStackTraceLines) : null
                }
            };

            return JsonSerializer.Serialize(logObject);
        }
    }
}
