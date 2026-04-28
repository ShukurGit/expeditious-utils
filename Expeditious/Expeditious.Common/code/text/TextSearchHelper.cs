
using System.Text.RegularExpressions;


namespace Expeditious.Common
{
    /// <summary>
    /// Утилиты для поиска слов в тексте.
    /// Поддерживает Unicode-буквы: русский, азербайджанский, английский и т.д.
    /// </summary>
    public static class TextSearchHelper
    {
        private static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(300);

        /// <summary>
        /// Проверяет, содержит ли текст указанное слово как отдельное слово.
        /// Не ищет внутри другого слова.
        /// </summary>
        public static bool ContainsWord(
            string? text,
            string? word,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (string.IsNullOrWhiteSpace(word))
                return false;

            return SplitToWords(text)
                .Any(x => string.Equals(x, word.Trim(), comparison));
        }

        /// <summary>
        /// Проверяет, содержит ли текст хотя бы одно слово из списка.
        /// </summary>
        public static bool ContainsAnyWord(
            string? text,
            IEnumerable<string>? words,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (words is null)
                return false;

            HashSet<string> wordSet = words
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToHashSet(StringComparerFromComparison(comparison));

            if (wordSet.Count == 0)
                return false;

            return SplitToWords(text).Any(wordSet.Contains);
        }

        /// <summary>
        /// Проверяет, содержит ли текст все слова из списка.
        /// </summary>
        public static bool ContainsAllWords(
            string? text,
            IEnumerable<string>? words,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (words is null)
                return false;

            HashSet<string> requiredWords = words
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToHashSet(StringComparerFromComparison(comparison));

            if (requiredWords.Count == 0)
                return false;

            HashSet<string> textWords = SplitToWords(text)
                .ToHashSet(StringComparerFromComparison(comparison));

            return requiredWords.All(textWords.Contains);
        }

        /// <summary>
        /// Считает количество вхождений слова в тексте.
        /// </summary>
        public static int CountWord(
            string? text,
            string? word,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            if (string.IsNullOrWhiteSpace(word))
                return 0;

            string normalizedWord = word.Trim();

            return SplitToWords(text)
                .Count(x => string.Equals(x, normalizedWord, comparison));
        }

        /// <summary>
        /// Заменяет отдельное слово в тексте.
        /// Не заменяет часть другого слова.
        /// </summary>
        public static string ReplaceWholeWord(
            string? text,
            string? word,
            string replacement,
            RegexOptions options = RegexOptions.IgnoreCase)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (string.IsNullOrWhiteSpace(word))
                return text;

            replacement ??= string.Empty;

            string pattern = BuildWholeWordPattern(word.Trim());

            return Regex.Replace(
                text,
                pattern,
                replacement,
                options,
                RegexTimeout);
        }

        /// <summary>
        /// Разбивает текст на слова.
        /// 
        /// Под словом понимается последовательность Unicode-букв и цифр.
        /// Поддерживает русский, азербайджанский, английский и другие языки.
        /// </summary>
        public static List<string> SplitToWords(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            return WordRegex()
                .Matches(text)
                .Select(match => match.Value)
                .ToList();
        }

        /// <summary>
        /// Ищет все слова, подходящие под указанный список искомых слов.
        /// </summary>
        public static List<string> FindWords(
            string? text,
            IEnumerable<string>? words,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            if (words is null)
                return new List<string>();

            HashSet<string> requiredWords = words
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToHashSet(StringComparerFromComparison(comparison));

            if (requiredWords.Count == 0)
                return new List<string>();

            return SplitToWords(text)
                .Where(requiredWords.Contains)
                .ToList();
        }

        private static string BuildWholeWordPattern(string word)
        {
            return $@"(?<![\p{{L}}\p{{N}}]){Regex.Escape(word)}(?![\p{{L}}\p{{N}}])";
        }

        private static StringComparer StringComparerFromComparison(StringComparison comparison)
        {
            return comparison switch
            {
                StringComparison.Ordinal => StringComparer.Ordinal,
                StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
                StringComparison.InvariantCulture => StringComparer.InvariantCulture,
                StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
                StringComparison.CurrentCulture => StringComparer.CurrentCulture,
                StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
                _ => StringComparer.OrdinalIgnoreCase
            };
        }

        private static readonly Regex WordRegexInstance = new(
            @"[\p{L}\p{N}]+",
            RegexOptions.Compiled,
            RegexTimeout);

        private static Regex WordRegex()
        {
            return WordRegexInstance;
        }
    }
}