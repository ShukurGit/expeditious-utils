using System;
using System.Collections.Generic;
using System.Text;

namespace Yeni.YeniLogging
{
    public interface ILogFileInfo
    {
        String Id { get; }
        String FolderName { get; }
        String FolderPath { get; }
        String FileName { get; }
        LogFilePathMode LogFilePathMode { get; }
        String LogFilePath { get; }
    }
}
