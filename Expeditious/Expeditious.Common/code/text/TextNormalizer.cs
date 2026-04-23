
using System.Text.RegularExpressions;


namespace Expeditious.Common
{
    public static partial class TextNormalizer
    {
        /// <summary>
        /// Заменяет все последовательности пробельных символов на один пробел и обрезает пробелы по краям строки.
        /// </summary>
        /// <param name="str">Исходная строка.</param>
        /// <returns>Строка без лишних пробелов.</returns>
        public static string RemoveRedundantSpaces(string str)
        {
            return Regex.Replace(str, @"\s+", " ").Trim();
        }



        /// <summary>
        /// Возвращает пустую строку, если переданное значение равно null.
        /// </summary>
        /// <param name="str">Исходная строка.</param>
        /// <returns>Пустую строку, если str = null, иначе возвращает str.</returns>
        public static string ConvertNullToEmpty(string str)
        {
            return str is null ? "" : str;
        }




        public static string TrimLastNewline(String text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            return text.EndsWith("\r\n") ? text.Substring(0, text.Length - 2) : text;
        }



        static public string? NewLineEndings(string text, string? replacementText = null)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;

            if (replacementText is null)
                return text.ReplaceLineEndings();  // Заменит все переносы строк на стандартные для текущей системы - Windows \r\n
            else
                return text.ReplaceLineEndings(replacementText);  // Все переносы строк заменятся на replacementText
        }
    }
}
