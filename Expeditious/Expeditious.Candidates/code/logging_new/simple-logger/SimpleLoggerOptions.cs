

namespace Expeditious.Candidates.SimpleLogger_a2
{
    public class SimpleLoggerOptions
    {
        public SimpleLogLevel MinLevel { get; set; } = SimpleLogLevel.Info;
        public bool IncludeStackTraceByDefault { get; set; } = false;
        public bool UseJson { get; set; } = true;
        public int MaxStackTraceLines { get; set; } = 15;
        public string LogDirectory { get; set; } = @"c:\Logs\";
    }
}
