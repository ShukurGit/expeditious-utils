
using System.Globalization;
using System.Text.RegularExpressions;


namespace Expeditious.Common
{

    public static class TextSearchAzHelper
    {
        private static readonly CultureInfo AzCulture =
            CultureInfo.GetCultureInfo("az-Latn-AZ");

        private static readonly CompareInfo AzCompareInfo =
            AzCulture.CompareInfo;

        private static readonly CompareOptions DefaultCompareOptions =
            CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace;

        private static readonly TimeSpan RegexTimeout =
            TimeSpan.FromMilliseconds(300);

        private static readonly Regex WordRegex = new(
            @"[\p{L}\p{N}]+",
            RegexOptions.Compiled,
            RegexTimeout);

        /// <summary>
        /// Разбивает текст на слова.
        /// Словом считается последовательность Unicode-букв и цифр.
        /// Подходит для азербайджанского, русского и английского текста.
        /// </summary>
        public static List<string> SplitToWords(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            return WordRegex
                .Matches(text)
                .Select(match => match.Value)
                .ToList();
        }

        /// <summary>
        /// Проверяет, содержит ли текст указанное слово как отдельное слово.
        /// Сравнение выполняется с учётом азербайджанской культуры.
        /// </summary>
        public static bool ContainsWord(string? text, string? word)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (string.IsNullOrWhiteSpace(word))
                return false;

            string normalizedWord = word.Trim();

            return SplitToWords(text)
                .Any(textWord => AreEqualAz(textWord, normalizedWord));
        }

        /// <summary>
        /// Проверяет, содержит ли текст хотя бы одно слово из списка.
        /// </summary>
        public static bool ContainsAnyWord(string? text, IEnumerable<string>? words)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (words is null)
                return false;

            List<string> searchWords = words
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToList();

            if (searchWords.Count == 0)
                return false;

            List<string> textWords = SplitToWords(text);

            return searchWords.Any(searchWord =>
                textWords.Any(textWord => AreEqualAz(textWord, searchWord)));
        }

        /// <summary>
        /// Проверяет, содержит ли текст все слова из списка.
        /// </summary>
        public static bool ContainsAllWords(string? text, IEnumerable<string>? words)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (words is null)
                return false;

            List<string> searchWords = words
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToList();

            if (searchWords.Count == 0)
                return false;

            List<string> textWords = SplitToWords(text);

            return searchWords.All(searchWord =>
                textWords.Any(textWord => AreEqualAz(textWord, searchWord)));
        }

        /// <summary>
        /// Считает количество вхождений отдельного слова в тексте.
        /// </summary>
        public static int CountWord(string? text, string? word)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            if (string.IsNullOrWhiteSpace(word))
                return 0;

            string normalizedWord = word.Trim();

            return SplitToWords(text)
                .Count(textWord => AreEqualAz(textWord, normalizedWord));
        }

        /// <summary>
        /// Находит в тексте все слова, совпадающие со словами из списка.
        /// Возвращает слова в том виде, в котором они были найдены в тексте.
        /// </summary>
        public static List<string> FindWords(string? text, IEnumerable<string>? words)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            if (words is null)
                return new List<string>();

            List<string> searchWords = words
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToList();

            if (searchWords.Count == 0)
                return new List<string>();

            return SplitToWords(text)
                .Where(textWord => searchWords.Any(searchWord => AreEqualAz(textWord, searchWord)))
                .ToList();
        }

        /// <summary>
        /// Заменяет отдельное слово в тексте.
        /// Важно: Regex IgnoreCase не всегда идеально работает с азербайджанским I/ı/İ/i,
        /// поэтому для максимально точной замены используется разбор текста на word/non-word сегменты.
        /// </summary>
        public static string ReplaceWholeWord(
            string? text,
            string? word,
            string? replacement)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (string.IsNullOrWhiteSpace(word))
                return text;

            replacement ??= string.Empty;

            string normalizedWord = word.Trim();

            return Regex.Replace(
                text,
                @"[\p{L}\p{N}]+",
                match =>
                {
                    return AreEqualAz(match.Value, normalizedWord)
                        ? replacement
                        : match.Value;
                },
                RegexOptions.None,
                RegexTimeout);
        }

        /// <summary>
        /// Сравнивает две строки по правилам азербайджанской культуры.
        /// </summary>
        public static bool AreEqualAz(string? first, string? second)
        {
            if (first is null && second is null)
                return true;

            if (first is null || second is null)
                return false;

            return AzCompareInfo.Compare(
                first.Trim(),
                second.Trim(),
                DefaultCompareOptions) == 0;
        }

        /// <summary>
        /// Нормализует слово в нижний регистр по правилам азербайджанской культуры.
        /// </summary>
        public static string NormalizeAzWord(string? word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return string.Empty;

            return word.Trim().ToLower(AzCulture);
        }
    }

}


/*
 string text = "Salam Bakı şəhəri. Şuşa və Gəncə gözəldir.";

bool hasBaku1 = TextSearchHelper.ContainsWord(text, "Bakı");   // true
bool hasBaku2 = TextSearchHelper.ContainsWord(text, "BAKI");   // true
bool hasBaku3 = TextSearchHelper.ContainsWord(text, "bakı");   // true
bool hasBaku4 = TextSearchHelper.ContainsWord(text, "BakI");   // true

bool hasShusha = TextSearchHelper.ContainsWord(text, "ŞUŞA");  // true
bool hasGanja = TextSearchHelper.ContainsWord(text, "GƏNCƏ");  // true
 */