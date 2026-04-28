

namespace Expeditious.Common.Combinatorics
{

    /// <summary>
    /// Генерирует все подмножества коллекции.
    /// Это также называется Power Set.
    ///
    /// Пример:
    /// [A, B, C]
    ///
    /// Результат:
    /// []
    /// [A]
    /// [B]
    /// [A, B]
    /// [C]
    /// [A, C]
    /// [B, C]
    /// [A, B, C]
    /// </summary>
    public static class PowerSetHelper
    {
        /// <summary>
        /// Возвращает все подмножества коллекции, включая пустое подмножество.
        /// 
        /// Внимание: количество результатов равно 2^n.
        /// Для больших коллекций использовать осторожно.
        /// </summary>
        public static List<List<T>> GetPowerSet<T>(IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            T[] items = source.ToArray();

            if (items.Length >= 31)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(source),
                    "Power set is too large. This implementation supports collections with fewer than 31 items.");
            }

            int totalCombinations = 1 << items.Length;

            var result = new List<List<T>>(totalCombinations);

            for (int mask = 0; mask < totalCombinations; mask++)
            {
                var subset = new List<T>();

                for (int index = 0; index < items.Length; index++)
                {
                    bool shouldIncludeItem = (mask & (1 << index)) != 0;

                    if (shouldIncludeItem)
                        subset.Add(items[index]);
                }

                result.Add(subset);
            }

            return result;
        }

        /// <summary>
        /// Возвращает все непустые подмножества коллекции.
        /// </summary>
        public static List<List<T>> GetNonEmptyPowerSet<T>(IEnumerable<T> source)
        {
            return GetPowerSet(source)
                .Where(x => x.Count > 0)
                .ToList();
        }
    }
}