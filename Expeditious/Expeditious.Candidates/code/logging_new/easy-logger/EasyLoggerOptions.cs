

namespace Expeditious.Candidates
{
    public class EasyLoggerOptions
    {
        public EasyLogLevel MinLevel { get; set; } = EasyLogLevel.Info;
        public bool IncludeStackTraceByDefault { get; set; } = false;
        public bool UseJson { get; set; } = true;
        public int MaxStackTraceLines { get; set; } = 15;
        public string LogRootDirectory { get; set; } = @"c:\Logs\";
        public string SpecificProjectsName { get; set; } = @"SomeProject";
    }
}
