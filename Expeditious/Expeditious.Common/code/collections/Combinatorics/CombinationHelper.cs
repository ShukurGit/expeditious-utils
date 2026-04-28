
namespace Expeditious.Common.Combinatorics
{



    // сравни с PowerSetHelper, результат тот же
    public static class CombinationHelper
    {
        /// <summary>
        /// Возвращает все непустые подмножества коллекции.
        /// Количество результатов: 2^n - 1.
        /// </summary>
        public static List<List<T>> GetNonEmptySubsets<T>(IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            T[] items = source.ToArray();

            if (items.Length >= 31)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(source),
                    "Too many items. This method supports fewer than 31 items.");
            }

            int total = 1 << items.Length;
            var result = new List<List<T>>(total - 1);

            for (int mask = 1; mask < total; mask++)
            {
                var subset = new List<T>();

                for (int index = 0; index < items.Length; index++)
                {
                    if ((mask & (1 << index)) != 0)
                        subset.Add(items[index]);
                }

                result.Add(subset);
            }

            return result;
        }

        /// <summary>
        /// Возвращает все непустые подмножества, отсортированные от длинных к коротким.
        /// </summary>
        public static List<List<T>> GetNonEmptySubsetsSortedByLengthDesc<T>(
            IEnumerable<T> source)
        {
            return GetNonEmptySubsets(source)
                .OrderByDescending(x => x.Count)
                .ToList();
        }
    }
}



/*
 usage
  
    var subsets = CombinationHelper.GetNonEmptySubsetsSortedByLengthDesc(new[] { "A", "B", "C" });
    string result = CollectionTextFormatter.ToMultilineText(subsets);

            
            [A, B, C]
            [A, B]
            [A, C]
            [B, C]
            [A]
            [B]
            [C]
            

*/