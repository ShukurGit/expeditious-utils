
using System.Collections.Generic;
using System.Text;

namespace Yeni.YeniLogging
{
    using System;
    using System.Reflection;


    public class Logs
    {
        private static /*readonly*/ String ArgLogRootFolder = @"c:\LOGS\SingletonLog";  // @"C:\TEKU_TEMP\LOGS";
        private static /*readonly*/ String ArgLogFileExtention = "log";
        private static /*readonly*/ String ArgProjectName = "SomeSingletonLog";
        private static /*readonly*/ LogFilePathMode ArgLogFilePathMode = LogFilePathMode.DynamicSeparatedByDate;


        public static void ConfigureLogger(String logRootFolder, String projectName, String logFileExtention, LogFilePathMode logFilePathMode = LogFilePathMode.DynamicSeparatedByDate)
        {
            if (_logConfigInstance == null)
            {
                Logs.ArgLogRootFolder = logRootFolder;
                Logs.ArgProjectName = projectName;
                Logs.ArgLogFileExtention = logFileExtention;
                Logs.ArgLogFilePathMode = logFilePathMode;
            }
        }


        private static ILogConfig _logConfigInstance;
        private static object syncRoot = new Object();

        private static ILogger _loggerInstance;
        private static object syncRoot2 = new Object();


        private static ILogConfig InstanceLogConfig
        {
            get
            {
                if (_logConfigInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (_logConfigInstance == null)
                        {
                            _logConfigInstance = new LogConfig(ArgLogRootFolder, ArgProjectName, ArgLogFileExtention, ArgLogFilePathMode);
                            _logConfigInstance.LogMode = LogMode.Debug;
                        }
                    }
                }
                return _logConfigInstance;
            }

        }


        public static ILogger InstanceLogger
        {
            get
            {
                if (_loggerInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (_loggerInstance == null)
                        {
                            _loggerInstance = new PureLogger(InstanceLogConfig);
                        }
                    }
                }
                return _loggerInstance;
            }

        }

        static public void AddInfo(String customMessage, MethodBase methodBase = null)
        {
            Logs.Log(customMessage, LogLevel.Information, methodBase: methodBase);
        }

        static public void AddWarn(String customMessage, MethodBase methodBase = null)
        {
            Logs.Log(customMessage, LogLevel.Warning, methodBase: methodBase);
        }

        static public void AddError(Exception ex, String customMessage, MethodBase methodBase = null)
        {
            Logs.Log(customMessage, LogLevel.Error, ex, methodBase: methodBase);
        }

        static private void Log(String customMessage, LogLevel logLevel, Exception ex = null, MethodBase methodBase = null)
        {
            switch (logLevel)
            {
                case LogLevel.Information:
                    Logs.InstanceLogger.LogInfo(customMessage, methodBase);
                    break;
                case LogLevel.Warning:
                    Logs.InstanceLogger.LogWarn(customMessage, methodBase);
                    break;
                case LogLevel.Error:
                    Logs.InstanceLogger.LogError(ex, customMessage, methodBase);
                    break;
                default:
                    Logs.InstanceLogger.LogInfo(customMessage, methodBase);
                    break;
            }
        }

        static public String GelLastLogRecord()
        {
            return Logs.InstanceLogger.LastLog;
        }

        static public String GetCurrentLogFilepath()
        {
            return Logs.InstanceLogger.LogConfig.LogFileInfo.LogFilePath;
        }

        static public void SetLogMode(LogMode logMode)
        {
            Logs.InstanceLogConfig.LogMode = logMode;

        }


        static public void AddLine(FillingChars fillingChars)
        {
            Logs.InstanceLogger.AddLine(fillingChars);
        }
    }
}
