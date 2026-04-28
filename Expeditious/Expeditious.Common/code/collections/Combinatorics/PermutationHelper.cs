

namespace Expeditious.Common.Combinatorics
{


    /// <summary>
    /// Генерирует перестановки элементов коллекции.
    ///
    /// Пример:
    /// [A, B, C]
    ///
    /// Результат:
    /// [A, B, C]
    /// [A, C, B]
    /// [B, A, C]
    /// [B, C, A]
    /// [C, A, B]
    /// [C, B, A]
    /// </summary>
    public static class PermutationHelper
    {
        /// <summary>
        /// Возвращает все перестановки элементов.
        /// 
        /// Внимание: количество результатов равно n!.
        /// Например:
        /// 10 элементов = 3 628 800 перестановок.
        /// 13 элементов = 6 227 020 800 перестановок.
        /// </summary>
        public static List<List<T>> GetPermutations<T>(IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            T[] items = source.ToArray();

            var result = new List<List<T>>();

            GeneratePermutations(items, 0, result);

            return result;
        }

        private static void GeneratePermutations<T>(
            T[] items,
            int startIndex,
            List<List<T>> result)
        {
            if (startIndex == items.Length - 1)
            {
                result.Add(items.ToList());
                return;
            }

            for (int i = startIndex; i < items.Length; i++)
            {
                Swap(items, startIndex, i);

                GeneratePermutations(items, startIndex + 1, result);

                Swap(items, startIndex, i);
            }
        }

        private static void Swap<T>(T[] array, int firstIndex, int secondIndex)
        {
            if (firstIndex == secondIndex)
                return;

            (array[firstIndex], array[secondIndex]) =
                (array[secondIndex], array[firstIndex]);
        }
    }
}

