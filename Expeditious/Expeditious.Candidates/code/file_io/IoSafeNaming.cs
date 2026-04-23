

namespace Expedite.Utils.FileIO
{
    static public class IoSafeNaming
    {
        private static readonly HashSet<char> RestrictedFileNameChars = Path.GetInvalidFileNameChars().ToHashSet();
        private static readonly HashSet<char> RestrictedPathChars = Path.GetInvalidPathChars().ToHashSet();


        private const char ReplacementChar = '_';


        public static string ToSafeFilePath(string inputFilePath) => ReplaceInvalidChars(inputFilePath, RestrictedPathChars);


        public static string ToSafeFileName(string inputFileName) => ReplaceInvalidChars(inputFileName, RestrictedFileNameChars);


        private static string ReplaceInvalidChars(string input, HashSet<char> invalidChars)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            char[] chars = input.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (invalidChars.Contains(chars[i]))
                    chars[i] = ReplacementChar;
            }

            return new string(chars);
        }
    }
}
