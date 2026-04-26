using System;
using System.Collections.Generic;
using System.Text;

namespace Yeni.YeniLogging
{
    public interface IErrorInfo
    {
        String CustomMessage { get; set; }
        String Message { get; }
        String NameMethod { get; }

        String NameProject { get; }
        string NameClassFull { get; }
        String NameClass { get; }
        string Namespace { get; }
        String StackTrace { get; }
        string ExecutableModule { get; }

        Boolean IsExceptionNull { get; }
    }
}
