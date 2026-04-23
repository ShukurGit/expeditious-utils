using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Expedite.Utils
{
    public static class ReflectionInfoBase // MethodFormatter
    {

        // System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
        public static string MethodInfoString(MethodBase method)
        {
            if (method == null)
                return string.Empty;

            var sb = new StringBuilder();

            // ===== Return type + async =====
            if (method is MethodInfo mi)
            {
                var returnType = mi.ReturnType;

                bool isAsync =
                    returnType == typeof(Task) ||
                    (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>)) ||
                    returnType == typeof(ValueTask) ||
                    (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ValueTask<>));

                if (isAsync)
                    sb.Append("async ");

                sb.Append(GetFriendlyTypeName(returnType));
                sb.Append(" ");
            }

            // ===== Class + method =====
            sb.Append(GetFriendlyTypeName(method.DeclaringType));
            sb.Append(".");
            sb.Append(method.Name);

            // ===== Generic method args =====
            if (method is MethodInfo mi2 && mi2.IsGenericMethod)
            {
                var genericArgs = mi2.GetGenericArguments()
                                    .Select(GetFriendlyTypeName);

                sb.Append("<");
                sb.Append(string.Join(", ", genericArgs));
                sb.Append(">");
            }

            // ===== Parameters =====
            var parameters = method.GetParameters()
                                   .Select(FormatParameter);

            sb.Append("(");
            sb.Append(string.Join(", ", parameters));
            sb.Append(")");

            return sb.ToString();
        }

        private static string FormatParameter(ParameterInfo p)
        {
            var modifier = "";

            if (p.IsOut)
                modifier = "out ";
            else if (p.ParameterType.IsByRef)
                modifier = "ref ";

            var type = p.ParameterType.IsByRef
                ? p.ParameterType.GetElementType()
                : p.ParameterType;

            return $"{modifier}{GetFriendlyTypeName(type)} {p.Name}";
        }

        private static string GetFriendlyTypeName(Type type)
        {
            if (type == null)
                return string.Empty;

            // Nullable<T>
            if (Nullable.GetUnderlyingType(type) is Type underlying)
                return $"{GetFriendlyTypeName(underlying)}?";

            // alias-типы
            if (_aliases.TryGetValue(type, out var alias))
                return alias;

            // массивы
            if (type.IsArray)
                return $"{GetFriendlyTypeName(type.GetElementType())}[]";

            // generic-типы
            if (type.IsGenericType)
            {
                var name = type.Name;
                var index = name.IndexOf('`');
                if (index > 0)
                    name = name.Substring(0, index);

                var args = type.GetGenericArguments()
                               .Select(GetFriendlyTypeName);

                return $"{name}<{string.Join(", ", args)}>";
            }

            return type.Name;
        }

        private static readonly Dictionary<Type, string> _aliases = new()
    {
        { typeof(int), "int" },
        { typeof(string), "string" },
        { typeof(bool), "bool" },
        { typeof(object), "object" },
        { typeof(void), "void" },
        { typeof(byte), "byte" },
        { typeof(char), "char" },
        { typeof(decimal), "decimal" },
        { typeof(double), "double" },
        { typeof(float), "float" },
        { typeof(long), "long" },
        { typeof(short), "short" }
    };
    }
}
