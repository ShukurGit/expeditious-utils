namespace Expeditious.Common.Combinatorics
{
    public static class PermutationOtherHelper
    {
        #region _  easy ready wrappers  _

        // ready wrappers

        // [1,2,3]     ->      [1,2,3] , [2,1,3] , [2,3,1] ...
        public static List<List<int>> PermutateIntArray(int[] elements)
        {
            List<List<int>> result = new List<List<int>>();

            foreach (var p in elements.GetPermutations())
            {
                result.Add(p.ToList());
            }

            return result;
        }



        // 'A', 'B', 'C'     ->      "ABC", "BAC", "ACB" ...
        public static List<string> PermutateStringsSymbols(char[] chars)
        {
            List<string> result = new List<string>();

            foreach (var p in chars.GetPermutations())
            {
                result.Add(string.Join("", p));
            }

            return result;
        }



        // ['A','B','C']     ->      ['A','B','C'], ['B','A','C'], ['A','C','B']
        public static List<List<char>> PermutateCharsArray(char[] chars)
        {
            List<List<char>> result = new List<List<char>>();

            foreach (var p in chars.GetPermutations())
            {
                result.Add(p.ToList());
            }

            return result;
        }



        #endregion _  easy ready wrappers  _



        #region _  core logic  _

        // core logic


        /// <summary>
        /// Генерирует все возможные перестановки коллекции с использованием алгоритма Хипа.
        /// </summary>
        public static IEnumerable<T[]> GetPermutations<T>(this T[] items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            // Работаем с копией, чтобы не изменять исходный массив
            T[] buffer = (T[])items.Clone();
            return Solve(buffer.Length, buffer);

            IEnumerable<T[]> Solve(int n, T[] array)
            {
                if (n <= 1)
                {
                    // Возвращаем копию текущего состояния массива
                    yield return (T[])array.Clone();
                    yield break;
                }

                for (int i = 0; i < n; i++)
                {
                    foreach (var p in Solve(n - 1, array))
                    {
                        yield return p;
                    }

                    // Логика перестановки алгоритма Хипа
                    if (n % 2 == 0)
                    {
                        Swap(array, i, n - 1);
                    }
                    else
                    {
                        Swap(array, 0, n - 1);
                    }
                }
            }
        }

        private static void Swap<T>(T[] array, int indexA, int indexB)
        {
            (array[indexA], array[indexB]) = (array[indexB], array[indexA]);
        }



        #endregion _  core logic  _
    }
}
