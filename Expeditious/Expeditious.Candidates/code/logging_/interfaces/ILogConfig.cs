using System;
using System.Collections.Generic;
using System.Text;

namespace Yeni.YeniLogging
{
    public interface ILogConfig
    {
        String RootFolder { get; }
        String FileExtention { get; }
        String ProjectName { get; }
        LogFilePathMode LogFilePathMode { get; }
        LogMode LogMode { get; set; }
        Boolean IsValidConfig { get; }


        ILogFileInfo LogFileInfo { get; }
    }
}
