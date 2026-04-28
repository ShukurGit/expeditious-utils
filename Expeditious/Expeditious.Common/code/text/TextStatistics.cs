

namespace Expeditious.Common
{
    public static class TextStatistics
    {
        static public int GetCountDigits(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return 0;
            return str.Count(c => char.IsDigit(c));
        }


        static public int GetCountLetters(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return 0;
            return str.Count(c => char.IsLetter(c));
        }


        static public bool HasDigits(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            return str.Any(c => char.IsDigit(c));
        }
    }
}
