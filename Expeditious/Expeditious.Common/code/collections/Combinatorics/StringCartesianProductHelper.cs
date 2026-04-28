

namespace Expeditious.Common.Combinatorics
{

    /// <summary>
    /// Генерирует декартово произведение строковых коллекций
    /// и склеивает элементы каждой комбинации в одну строку.
    /// 
    /// Пример:
    /// [ ["A", "B"], ["1", "2"] ]
    /// 
    /// Результат:
    /// A1
    /// A2
    /// B1
    /// B2
    /// </summary>
    public static class StringCartesianProductHelper
    {
        /// <summary>
        /// Возвращает все строковые комбинации, склеивая элементы по порядку.
        /// </summary>
        /// <param name="collections">Коллекции строк.</param>
        /// <returns>Список склеенных строковых комбинаций.</returns>
        public static List<string> GetConcatenatedProduct(
            IEnumerable<IEnumerable<string>> collections)
        {
            if (collections is null)
                throw new ArgumentNullException(nameof(collections));

            IEnumerable<string> result = new[] { string.Empty };

            foreach (IEnumerable<string> collection in collections)
            {
                if (collection is null)
                    throw new ArgumentException("Inner collection cannot be null.", nameof(collections));

                result = result.SelectMany(prefix =>
                    collection.Select(value => prefix + value));
            }

            return result.ToList();
        }
    }
}