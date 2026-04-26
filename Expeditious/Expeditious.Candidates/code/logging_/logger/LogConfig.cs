
using System.Collections.Generic;
using System.Text;

namespace Yeni.YeniLogging
{
    using System;


    public class LogConfig : ILogConfig
    {
        public String RootFolder { get; private set; }
        public String FileExtention { get; private set; }
        public String ProjectName { get; private set; }
        public LogFilePathMode LogFilePathMode { get; }
        public LogMode LogMode { get; set; }
        public Boolean IsValidConfig { get { return this.IsValidConfiguration(); } }

        public ILogFileInfo LogFileInfo { get; }


        public LogConfig(String rootFolder, String projName = null, String fileExt = null, LogFilePathMode logFilePathMode = LogFilePathMode.DynamicSeparatedByDate)
        {
            this.RootFolder = HelpersIO.CheckFolder(rootFolder) ? rootFolder : ConstLogs.DEFAULT_ROOT_FOLDER;
            this.FileExtention = GetSafeTextOrAlternative(fileExt, ConstLogs.DEFAULT_FILE_EXTENTION);
            this.ProjectName = GetSafeTextOrAlternative(projName, ConstLogs.DEFAULT_PROJECT_NAME);
            this.LogFilePathMode = logFilePathMode;
            this.LogMode = LogMode.Debug;

            this.LogFileInfo = new LogFileInfo(this);
        }


        private Boolean IsValidConfiguration()
        {
            return HelpersIO.CheckFolder(this.RootFolder) && !String.IsNullOrWhiteSpace(this.FileExtention) && !String.IsNullOrWhiteSpace(this.ProjectName);
        }


        private String GetSafeTextOrAlternative(String text, String alternativeText)
        {
            if (String.IsNullOrWhiteSpace(text)) return alternativeText;

            String substitution = "_";
            String safeText = HelpersIO.ToSafeTextIoPath(text, replacementInvalids: substitution, replacementNullEmpty: substitution);
            if (safeText == substitution) return alternativeText;

            return text;
        }

    }
}
