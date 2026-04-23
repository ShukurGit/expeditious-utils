

using System.Text;

namespace Expeditious.Common
{
    public static partial class TextNormalizer
    {
        static public class ProblematicPunctuationChars
        {
            public static string Replace(string text)
            {
                if (string.IsNullOrWhiteSpace(text)) return text;

                StringBuilder sb = new(text);

                // HTML at first 
                sb.Replace("&quot;", "″");
                sb.Replace("&apos;", "’");

                // Iterate through the string once to replace single characters
                for (int i = 0; i < sb.Length; i++)
                {
                    char c = sb[i];
                    sb[i] = c switch
                    {
                        '"' or '«' or '»' => '″',
                        '\'' or '`' => '’',
                        ',' => '‚',
                        _ => c
                    };
                }

                return sb.ToString();
            }


            public static string Restore(string text)
            {
                if (string.IsNullOrWhiteSpace(text)) return text;

                StringBuilder sb = new(text);

                for (int i = 0; i < sb.Length; i++)
                {
                    char c = sb[i];
                    sb[i] = c switch
                    {
                        '″' => '"',
                        '’' => '\'',
                        '‚' => ',',
                        _ => c
                    };
                }
                return sb.ToString();
            }
        }
    }
}