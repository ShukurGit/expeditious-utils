
using System.Text;


namespace Expeditious.Common
{
    public static class TextModifier
    {
        public static string ReplaceMultipleChars(string iniText, IReadOnlyDictionary<char, char> mappedChars)
        {
            if (string.IsNullOrWhiteSpace(iniText) || mappedChars == null || mappedChars.Count == 0) return iniText;

            var result = new char[iniText.Length];

            for (int i = 0; i < iniText.Length; i++)
            {
                char c = iniText[i];
                result[i] = mappedChars.TryGetValue(c, out var r) ? r : c;
            }

            return new string(result);
        }



        public static string ReplaceMultipleChars(string iniText, IReadOnlyDictionary<char, string> mappedChars)
        {
            if (string.IsNullOrWhiteSpace(iniText) || mappedChars == null || mappedChars.Count == 0)
                return iniText;

            var result = new StringBuilder(iniText.Length);

            for (int i = 0; i < iniText.Length; i++)
            {
                char c = iniText[i];

                if (mappedChars.TryGetValue(c, out var replacement))
                    result.Append(replacement);
                else
                    result.Append(c);
            }

            return result.ToString();
        }


        public static string InsertCharAtInterval(string text, int interval, char ch)
        {
            if (string.IsNullOrEmpty(text) || interval <= 0 || interval >= text.Length)
                return text;

            int inserts = (text.Length - 1) / interval;
            int newLength = text.Length + inserts;

            var result = new char[newLength];

            int src = 0;
            int dst = 0;
            int counter = 0;

            while (src < text.Length)
            {
                result[dst++] = text[src++];
                counter++;

                if (counter == interval && src < text.Length)
                {
                    result[dst++] = ch;
                    counter = 0;
                }
            }

            return new string(result);
        }



        public static string InsertRandomCharAtInterval(string text, int interval, string alphabet)
        {
            if (string.IsNullOrEmpty(text) || interval <= 0 || interval >= text.Length)
                return text;

            if (string.IsNullOrEmpty(alphabet))
                throw new ArgumentException("Alphabet must not be empty", nameof(alphabet));

            int inserts = (text.Length - 1) / interval;
            int newLength = text.Length + inserts;

            var result = new char[newLength];

            int src = 0;
            int dst = 0;
            int counter = 0;

            var rnd = Random.Shared;

            while (src < text.Length)
            {
                result[dst++] = text[src++];
                counter++;

                if (counter == interval && src < text.Length)
                {
                    result[dst++] = alphabet[rnd.Next(alphabet.Length)];
                    counter = 0;
                }
            }

            return new string(result);
        }


        public static string RemoveEachNthLetter(string text, int n)
        {
            if (string.IsNullOrEmpty(text) || n <= 1 || n > text.Length)
                return text;

            int newLength = text.Length - (text.Length / n);
            var result = new char[newLength];

            int dst = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if ((i + 1) % n != 0)
                {
                    result[dst++] = text[i];
                }
            }

            return new string(result);
        }




        //public static string InsertCharAtInterval2(string iniText, int partLettersCount, char letter)
        //{
        //    if (partLettersCount >= iniText.Length) return iniText;

        //    StringBuilder result = new StringBuilder(iniText);

        //    for (int i = partLettersCount; i < result.Length; i = i + partLettersCount + 1)
        //        result.Insert(i, letter.ToString(), 1);

        //    return result.ToString();
        //}


        //static public string RemoveEachNthLetter2(string iniText, int n)
        //{
        //    if (n >= iniText.Length) return iniText;

        //    List<char> chars = iniText.ToCharArray().ToList();
        //    List<char> chars2 = new List<char>();

        //    chars2.Add(chars[0]);

        //    for (int i = 1; i < chars.Count; i++)
        //    {
        //        if ((i + 1) % n != 0)
        //            chars2.Add(chars[i]);
        //    }

        //    return string.Join("", chars2);
        //}
    }
}