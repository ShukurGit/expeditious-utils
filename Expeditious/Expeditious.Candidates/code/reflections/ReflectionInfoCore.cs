using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Expedite.Utils
{
    static public class ReflectionInfoCore
    {
        // System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
        static public String MethodInfoString(MethodBase methodBase)
        {
            if (methodBase != null)
                // return String.Format("{0}.{1} ({2})", methodBase.ReflectedType.FullName, methodBase.Name, String.Join(", ", methodBase.GetParameters().Select(o => String.Format("{0} {1}", o.ParameterType, o.Name)).ToArray()));
                return $"{methodBase.ReflectedType.FullName}.{methodBase.Name}({String.Join(", ", methodBase.GetParameters().Select(o => $"{o.ParameterType} {o.Name}"))})";
            return String.Empty;
        }
    }
}
