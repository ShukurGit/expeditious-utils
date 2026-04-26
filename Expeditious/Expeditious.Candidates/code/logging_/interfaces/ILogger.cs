
using System.Collections.Generic;
using System.Text;

namespace Yeni.YeniLogging
{
    using System;
    using System.Reflection;


    public interface ILogger
    {
        ILogConfig LogConfig { get; }
        String LastLog { get; }

        void Log(String message, LogLevel logLevel, MethodBase methodBase);
        void LogInfo(String message, MethodBase methodBase);
        void LogWarn(String message, MethodBase methodBase);
        void LogError(Exception ex, String customMessage, MethodBase methodBase);

        void AddLine(FillingChars fillingChars);
    }
}
