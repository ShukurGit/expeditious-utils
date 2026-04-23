

namespace Expeditious.Common
{
    public static partial class TextTransformer
    {
        static public string ToReverse(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;
            else
            {
                char[] charArray = text.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }
        }
    }
}


