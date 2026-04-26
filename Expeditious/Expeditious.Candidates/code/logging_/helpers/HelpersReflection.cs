
using System.Collections.Generic;
using System.Text;

namespace Yeni.YeniLogging
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class HelpersReflection
    {
        // System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
        public static String DefineCalledMethodInfo(MethodBase methodBase)
        {
            if (methodBase != null)
                // return String.Format("{0}.{1}({2})", methodBase.ReflectedType.FullName, methodBase.Name, string.Join(", ", methodBase.GetParameters().Select(o => string.Format("{0} {1}", o.ParameterType, o.Name)).ToArray()));
                return $"{methodBase.ReflectedType.FullName}.{methodBase.Name}({String.Join(", ", methodBase.GetParameters().Select(o => $"{o.ParameterType} {o.Name}"))})";
            else
                return String.Empty;
        }
    }
}
