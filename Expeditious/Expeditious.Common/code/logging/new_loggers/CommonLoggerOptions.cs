using System;
using System.Collections.Generic;
using System.Text;

namespace Expeditious.Common
{
    public class CommonLoggerOptions
    {
        public CommonLogLevel MinLevel { get; set; } = CommonLogLevel.Info;
        public bool IncludeStackTraceByDefault { get; set; } = false;
        public bool UseJson { get; set; } = true;
        public int MaxStackTraceLines { get; set; } = 15;
        public string LogRootDirectory { get; set; } = @"c:\Logs\";
        public string SpecificProjectName { get; set; } = @"MyProject";

        public string SpecificProjectFolder { get { return this.GetSpecificProjectFolder(); } }



        private string GetSpecificProjectFolder()
        {
            return string.IsNullOrWhiteSpace(this.SpecificProjectName) ? this.LogRootDirectory :
                System.IO.Path.Combine(this.LogRootDirectory, this.SpecificProjectName);
        }
    }
}
