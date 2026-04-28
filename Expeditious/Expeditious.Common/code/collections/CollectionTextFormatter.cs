

namespace Expeditious.Common
{
    /// <summary>
    /// Преобразует коллекции списков в многострочный текст.
    /// Каждый внутренний список выводится отдельной строкой.
    /// </summary>
    public static class CollectionTextFormatter
    {
        /// <summary>
        /// Преобразует список списков в один текст с переводами строк.
        ///
        /// Пример:
        /// [A, B]
        /// [A, C]
        /// [B, C]
        /// </summary>
        /// <typeparam name="T">Тип элемента.</typeparam>
        /// <param name="source">Коллекция списков.</param>
        /// <param name="separator">Разделитель элементов внутри строки.</param>
        /// <param name="useBrackets">Добавлять квадратные скобки.</param>
        /// <returns>Многострочный текст.</returns>
        public static string ToMultilineText<T>(
            IEnumerable<IEnumerable<T>> source,
            string separator = ", ",
            bool useBrackets = true)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            var lines = source.Select(row =>
            {
                string line = string.Join(
                    separator,
                    row?.Select(x => x?.ToString() ?? string.Empty)
                    ?? Enumerable.Empty<string>());

                return useBrackets
                    ? $"[{line}]"
                    : line;
            });

            return string.Join(Environment.NewLine, lines);
        }
    }
}