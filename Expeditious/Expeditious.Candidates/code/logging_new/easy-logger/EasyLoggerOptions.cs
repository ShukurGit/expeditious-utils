

namespace Expeditious.Candidates
{
    public class EasyLoggerOptions
    {
        public EasyLogLevel MinLevel { get; set; } = EasyLogLevel.Info;
        public bool IncludeStackTraceByDefault { get; set; } = false;
        public bool UseJson { get; set; } = true;
        public int MaxStackTraceLines { get; set; } = 15;
        public string LogRootDirectory { get; set; } = @"c:\Logs\";
        public string SpecificProjectName { get; set; } = @"SomeProject";

        public string SpecificProjectFolder { get { return this.GetSpecificProjectFolder(); } }



        private string GetSpecificProjectFolder()
        {
            return string.IsNullOrWhiteSpace(this.SpecificProjectName) ? this.LogRootDirectory :
                System.IO.Path.Combine(this.LogRootDirectory, this.SpecificProjectName);
        }
    }
}
