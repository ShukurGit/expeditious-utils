
using System.Collections.Generic;
using System.Text;

namespace Yeni.YeniLogging
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;


    public class PureLogger : ILogger
    {
        private readonly String _error = "ERROR";
        private readonly String _warn = "WARN ";
        private readonly String _info = "INFO ";
        private readonly static String NL = Environment.NewLine;
        private readonly static String FORMAT_DATETIME = "yyyy.MM.dd  HH:mm:ss.fff";

        private ILogConfig _logConfig;
        private String _lastLog = "";
        public ILogConfig LogConfig { get { return this._logConfig; } }

        public String LastLog { get { return this._lastLog; } }

        public PureLogger(ILogConfig logConfig)
        {
            SetLogConfig(logConfig);
        }


        public void SetLogConfig(ILogConfig logConfig)
        {
            if (logConfig == null || !logConfig.IsValidConfig)
                throw new Exception($"Logger config is invalid.");

            this._logConfig = logConfig;
        }

        public void LogError(Exception ex, String customMessage, MethodBase methodBase = null)
        {
            ErrorInfo errorInfo = new ErrorInfo(ex, customMessage);
            String message = errorInfo.ToStr(this.LogConfig.LogMode == LogMode.Debug);
            this.Log(message, LogLevel.Error, methodBase);
        }

        public void LogInfo(String message, MethodBase methodBase = null)
        {
            this.Log(message, LogLevel.Information, methodBase);
        }

        public void LogWarn(String message, MethodBase methodBase = null)
        {
            this.Log(message, LogLevel.Warning, methodBase);
        }

        public void Log(String message, LogLevel logLevel, MethodBase methodBase = null)
        {
            String filePath = this.LogConfig.LogFileInfo.LogFilePath;

            String lvl = "";
            switch (logLevel)
            {
                case LogLevel.Information:
                    lvl = this._info;
                    break;
                case LogLevel.Warning:
                    lvl = this._warn;
                    break;
                case LogLevel.Error:
                    lvl = this._error;
                    break;
                default:
                    break;
            }

            if (this.LogConfig.LogMode == LogMode.Debug && methodBase != null)
                message = $"{message} {NL}\t\t[{HelpersReflection.DefineCalledMethodInfo(methodBase)}]";

            String log = $"{NL}[{lvl}]\t[{DateTime.Now.ToString(FORMAT_DATETIME)}]\t{message}";
            log = HelpersIO.ToSafeTextDb(log);
            this._lastLog = log;

            try
            {
                File.AppendAllText(filePath, log);
            }
            catch (Exception e)
            {
                // throw;
            }

        }


        public void AddLine(FillingChars fillingChars)
        {
            Char ch = ' ';

            switch (fillingChars)
            {
                case FillingChars.EmptyLine:
                    ch = ' ';
                    break;
                case FillingChars.Stars:
                    ch = '*';
                    break;
                case FillingChars.DashLine:
                    ch = '-';
                    break;
                case FillingChars.SolidLine:
                    ch = '_';
                    break;
                default:
                    break;
            }
            try
            {

                String line = Environment.NewLine + String.Concat(Enumerable.Repeat(ch, 64));
                File.AppendAllText(this.LogConfig.LogFileInfo.LogFilePath, line);
            }
            catch (Exception e)
            {
                // throw;
            }
        }
    }
}
