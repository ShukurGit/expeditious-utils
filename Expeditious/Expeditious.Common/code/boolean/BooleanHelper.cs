


namespace Expeditious.Common
{
    public class BooleanHelper
    {
        static public readonly List<string> VALUES_TRUE = new List<string>() { "1", "true", "yes", "y" };
        static public readonly List<string> VALUES_FALSE = new List<string>() { "0", "false", "no", "n" };

        static public bool? ParseBooleanFromString(string? strValue)
        {
            if (String.IsNullOrWhiteSpace(strValue)) return null;

            string val = strValue.Trim().ToLower();

            if (VALUES_TRUE.Contains(val)) return true;
            if (VALUES_FALSE.Contains(val)) return false;

            throw new Exception($"ERROR: Can`t parse boolean value from string {strValue}");
        }


        static public bool? ParseBooleanFromInt(int? intValue)
        {
            if (intValue == null) return null;
            if (intValue == 1) return true;
            if (intValue == 0) return false;

            throw new Exception($"ERROR: Can`t parse boolean value from integer {intValue}");
        }
    }
}
