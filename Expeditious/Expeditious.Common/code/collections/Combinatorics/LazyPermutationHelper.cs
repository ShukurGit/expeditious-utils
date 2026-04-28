

namespace Expeditious.Common.code.collections.Combinatorics
{
    /// <summary>
    /// Ленивый генератор перестановок.
    /// 
    /// В отличие от PermutationHelper.GetPermutations,
    /// не складывает сразу все результаты в память.
    /// Это лучше для больших коллекций.
    /// </summary>
    public static class LazyPermutationHelper
    {
        /// <summary>
        /// Возвращает перестановки по одной через yield return.
        /// </summary>
        public static IEnumerable<List<T>> GetPermutationsLazy<T>(IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            T[] items = source.ToArray();

            foreach (List<T> permutation in Generate(items, 0))
                yield return permutation;
        }

        private static IEnumerable<List<T>> Generate<T>(T[] items, int startIndex)
        {
            if (startIndex == items.Length - 1)
            {
                yield return items.ToList();
                yield break;
            }

            for (int i = startIndex; i < items.Length; i++)
            {
                Swap(items, startIndex, i);

                foreach (List<T> permutation in Generate(items, startIndex + 1))
                    yield return permutation;

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

