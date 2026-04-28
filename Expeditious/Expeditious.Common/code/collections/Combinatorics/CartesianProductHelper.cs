

namespace Expeditious.Common.Combinatorics
{

    /// <summary>
    /// Генерирует декартово произведение нескольких коллекций.
    /// 
    /// Пример:
    /// [ [A, B], [1, 2] ]
    /// 
    /// Результат:
    /// [A, 1]
    /// [A, 2]
    /// [B, 1]
    /// [B, 2]
    /// </summary>
    public static class CartesianProductHelper
    {
        /// <summary>
        /// Возвращает все возможные комбинации, где из каждой внутренней коллекции
        /// берётся ровно один элемент.
        /// </summary>
        /// <typeparam name="T">Тип элементов.</typeparam>
        /// <param name="collections">Список коллекций.</param>
        /// <returns>Список комбинаций.</returns>
        /// <exception cref="ArgumentNullException">
        /// Если входная коллекция равна null.
        /// </exception>
        public static List<List<T>> GetCartesianProduct<T>(
            IEnumerable<IEnumerable<T>> collections)
        {
            if (collections is null)
                throw new ArgumentNullException(nameof(collections));

            IEnumerable<List<T>> result = new[] { new List<T>() };

            foreach (IEnumerable<T> collection in collections)
            {
                if (collection is null)
                    throw new ArgumentException("Inner collection cannot be null.", nameof(collections));

                result = result.SelectMany(existingCombination =>
                    collection.Select(item =>
                    {
                        var newCombination = new List<T>(existingCombination)
                        {
                        item
                        };

                        return newCombination;
                    }));
            }

            return result.ToList();
        }
    }
}