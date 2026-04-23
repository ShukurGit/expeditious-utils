
using System.Text;
using System.Text.RegularExpressions;


namespace Expeditious.Common
{
    public static partial class TextTransformer
    {
        public static class UnicodeEscaper
        {

            /// <summary>
            /// Кодирует строку в \uXXXX (или \UXXXXXXXX для расширенных символов)
            /// </summary>
            public static string To(string text, bool nonAsciiOnly = true)
            {
                if (string.IsNullOrEmpty(text))
                    return text;

                var sb = new StringBuilder();

                foreach (var rune in text.EnumerateRunes())
                {
                    if (nonAsciiOnly && rune.Value <= 0x7F)
                    {
                        sb.Append((char)rune.Value);
                        continue;
                    }

                    if (rune.Value <= 0xFFFF)
                    {
                        sb.Append("\\u");
                        sb.Append(rune.Value.ToString("X4"));
                    }
                    else
                    {
                        sb.Append("\\U");
                        sb.Append(rune.Value.ToString("X8"));
                    }
                }

                return sb.ToString();
            }



            /// <summary>
            /// Декодирует \uXXXX и \UXXXXXXXX обратно в строку
            /// </summary>
            public static string From(string text)
            {
                if (string.IsNullOrEmpty(text))
                    return text;

                // \UXXXXXXXX (сначала длинные!)
                text = Regex.Replace(text, @"\\U([0-9A-Fa-f]{8})", m =>
                {
                    var code = Convert.ToInt32(m.Groups[1].Value, 16);
                    return char.ConvertFromUtf32(code);
                });

                // \uXXXX
                text = Regex.Replace(text, @"\\u([0-9A-Fa-f]{4})", m =>
                {
                    var code = Convert.ToInt32(m.Groups[1].Value, 16);
                    return ((char)code).ToString();
                });

                return text;
            }
        }
    }
    
}
