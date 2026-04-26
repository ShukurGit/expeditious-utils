
using System.Collections.Generic;
using System.Text;

namespace Yeni.YeniLogging
{
    using System;



    public class ErrorInfo : IErrorInfo
    {
        static private readonly String NL = Environment.NewLine;

        public String CustomMessage { get; set; }
        public String Message { get; private set; }
        public String NameMethod { get; private set; }

        public String NameProject { get; private set; }
        public String NameClassFull { get; private set; }
        public String NameClass { get; private set; }
        public String Namespace { get; private set; }
        public String StackTrace { get; private set; }
        public String ExecutableModule { get; private set; }

        public Boolean IsExceptionNull { get; private set; }


        // constructor
        public ErrorInfo(Exception ex, String customMessage)
        {
            this.CustomMessage = customMessage;

            if (ex == null)
            {
                this.IsExceptionNull = true;
            }
            else
            {
                this.IsExceptionNull = false;
                this.CustomMessage = customMessage;
                this.Message = ex.Message;
                this.StackTrace = ex.StackTrace;
                this.NameMethod = ex.TargetSite?.Name;
                this.NameClass = ex.TargetSite?.DeclaringType?.Name;
                this.NameClassFull = ex.TargetSite?.DeclaringType?.FullName;

                this.Namespace = ex.TargetSite?.DeclaringType?.Namespace;
                this.NameProject = ex.Source;
                this.ExecutableModule = ex.TargetSite?.Module?.FullyQualifiedName;
            }
        }


        public String ToStr(Boolean isDebugMode)
        {
            if (isDebugMode)
                return ToStrDebug();
            else
                return ToStrInfo();
        }


        private String ToStrInfo()
        {
            if (this.IsExceptionNull)
                return $"{this.CustomMessage}";
            else
                return $"{this.CustomMessage}{NL}\t\tException:\t{this.Message}{NL}\t\tMethod:\t\t{this.NameClassFull}.{this.NameMethod}";
        }


        private String ToStrDebug()
        {
            if (this.IsExceptionNull)
            {
                return $"{this.CustomMessage}";
            }
            else
            {
                String result = this.ToStrInfo();
                result += $"{NL}\t\tNamespace:\t{this.Namespace}{NL}\t\tNameProject:\t{this.NameProject}{NL}\t\tExecutable:\t{this.ExecutableModule}";
                result += $"{NL}\t\tStackTrace:\t{NL}\t\t\t {this.StackTrace}{NL}";
                return result;
            }
        }
    }
}
