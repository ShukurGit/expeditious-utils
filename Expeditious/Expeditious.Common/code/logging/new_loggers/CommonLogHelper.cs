using System;
using System.Collections.Generic;
using System.Text;

namespace Expeditious.Common
{
    public static class CommonLogHelper
    {
        static public DateTime TimestampEverywhereAze() 
        {
            // get safely azeri time everywhere
            return TimeZoneInfo.ConvertTime((DateTimeOffset)DateTimeOffset.Now, OperatingSystem.IsWindows()
                    ? TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time")
                    : TimeZoneInfo.FindSystemTimeZoneById("Asia/Baku")).DateTime;
        }


        static public string TimestampEverywhereAzeStr()
        {
            return TimestampEverywhereAze().ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
        }


        public static string GetFilePath(CommonLoggerOptions options)
        {
            string extension = options.UseJson ? "json" : "txt";
            return Path.Combine(options.SpecificProjectFolder, $"log{options.SpecificProjectName}_{TimestampEverywhereAze():yyyy-MM-dd}.{extension}");
        }


        public static string TrimStackTrace(string stack, int maxStackTraceLines)
        {
            var lines = stack.Split(Environment.NewLine);

            if (lines.Length <= maxStackTraceLines)
                return stack;

            return string.Join(Environment.NewLine, lines.Take(maxStackTraceLines))
                   + Environment.NewLine + "... (truncated)";
        }
    }
}
