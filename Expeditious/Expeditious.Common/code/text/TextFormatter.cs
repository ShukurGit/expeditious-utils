

namespace Expeditious.Common
{
    public static class TextFormatter
    {
        static public string SurroundWithChars(string str, char startChar, char endChar, bool shiftAsideBySpaces = false)
        {
            if (str != null)
            {
                string spaces = shiftAsideBySpaces ? " " : "";
                return string.Format("{0}{1}{2}{1}{3}", startChar, spaces, str, endChar);
            }
            return str;
        }


        static public String RemoveSurroundedChars(string str, char startChar, char endChar, bool trim = true)
        {
            if (trim)
                str = str.Trim();
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                return str;

            if (str[0] == startChar)
                str = str.Substring(1, str.Length - 1);
            if (str[str.Length - 1] == endChar)
                str = str.Substring(0, str.Length - 1);

            return str.Trim();
        }
    }
}
