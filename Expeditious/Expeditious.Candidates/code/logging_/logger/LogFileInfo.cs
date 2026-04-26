
using System.Collections.Generic;
using System.Text;

namespace Yeni.YeniLogging
{
    using System;
    using System.IO;


    public class LogFileInfo : ILogFileInfo
    {
        private readonly static String FILE_DATE = "yyyy_MM_dd";

        private String _logFilePath;
        private String _suffixDate;
        private String _fileExtention;


        public String Id { get; }
        public String FolderName { get; }
        public String FolderPath { get; }
        public String FileName { get { return $"{this.FolderName}_{_suffixDate}.{this._fileExtention}"; } }

        public LogFilePathMode LogFilePathMode { get; }

        public String LogFilePath { get { return this.GetLogFilePath(); } }



        public LogFileInfo(ILogConfig logConfig)
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.LogFilePathMode = logConfig.LogFilePathMode;
            this.FolderName = logConfig.ProjectName;
            this._fileExtention = logConfig.FileExtention;

            this.FolderPath = Path.Combine(logConfig.RootFolder, logConfig.ProjectName);
            HelpersIO.CheckFolder(this.FolderPath);

            if (logConfig.LogFilePathMode == LogFilePathMode.DynamicSeparatedByDate)
                this._suffixDate = DateTime.Now.ToString(FILE_DATE);
            else // if (logConfig.LogFilePathMode == LogFilePathMode.PermanentPath)
                this._suffixDate = DateTime.Now.Ticks.ToString();

            this._logFilePath = Path.Combine(this.FolderPath, this.FileName);
        }


        public String GetLogFilePath()
        {
            if (this.LogFilePathMode == LogFilePathMode.DynamicSeparatedByDate)
            {
                if (DateTime.Now.ToString(FILE_DATE) != this._suffixDate)
                {
                    this._suffixDate = DateTime.Now.ToString(FILE_DATE);
                    this._logFilePath = Path.Combine(this.FolderPath, this.FileName);
                }

            }

            return this._logFilePath;
        }

    }
}
