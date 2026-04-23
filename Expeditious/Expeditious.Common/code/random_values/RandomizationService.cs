
using System.Security.Cryptography;


namespace Expeditious.Common
{
    public static class RandomizationService
    {
        public static int NextInt(int minInclusive, int maxExclusive, bool secure = false)
        {
            if (minInclusive >= maxExclusive)
                throw new ArgumentOutOfRangeException(nameof(minInclusive));

            return secure
                ? RandomNumberGenerator.GetInt32(minInclusive, maxExclusive)
                : Random.Shared.Next(minInclusive, maxExclusive);
        }


        public static int[] NextInts(int count, int minInclusive, int maxExclusive, bool secure = false)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (minInclusive >= maxExclusive)
                throw new ArgumentOutOfRangeException(nameof(minInclusive));

            var result = new int[count];

            if (!secure)
            {
                var rnd = Random.Shared;
                for (int i = 0; i < count; i++)
                    result[i] = rnd.Next(minInclusive, maxExclusive);

                return result;
            }

            for (int i = 0; i < count; i++)
            {
                result[i] = RandomNumberGenerator.GetInt32(minInclusive, maxExclusive);
            }

            return result;
        }


        public static char NextChar(string alphabet, bool secure = false)
        {
            if (string.IsNullOrEmpty(alphabet))
                throw new ArgumentException("Alphabet must not be empty", nameof(alphabet));

            int index = secure
                ? RandomNumberGenerator.GetInt32(alphabet.Length)
                : Random.Shared.Next(alphabet.Length);

            return alphabet[index];
        }



        public static string NextString(int length, string alphabet, bool secure = false)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            if (string.IsNullOrWhiteSpace(alphabet))
                throw new ArgumentException("Alphabet must not be empty", nameof(alphabet));

            if (length == 0)
                return string.Empty;

            var result = new char[length];

            if (secure)
            {
                for (int i = 0; i < length; i++)
                {
                    int idx = RandomNumberGenerator.GetInt32(alphabet.Length);
                    result[i] = alphabet[idx];
                }
            }
            else
            {
                var rnd = Random.Shared;

                for (int i = 0; i < length; i++)
                {
                    result[i] = alphabet[rnd.Next(alphabet.Length)];
                }
            }

            return new string(result);
        }


        public static bool NextBool(bool secure = false)
        {
            return secure
                ? RandomNumberGenerator.GetInt32(0, 2) == 1
                : Random.Shared.Next(2) == 1;
        }


        public static byte NextByte(bool secure = false)
        {
            if (!secure)
                return (byte)Random.Shared.Next(0, 256);

            Span<byte> b = stackalloc byte[1];
            RandomNumberGenerator.Fill(b);
            return b[0];
        }


        public static byte[] NextBytes(int length, bool secure = false)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var bytes = new byte[length];

            if (secure)
                RandomNumberGenerator.Fill(bytes);
            else
                Random.Shared.NextBytes(bytes);

            return bytes;
        }


        public static double NextDouble(bool secure = false)
        {
            if (!secure)
                return Random.Shared.NextDouble();

            // 53 случайных бита → [0,1)
            ulong value = NextUInt64Secure() >> 11; // оставляем 53 бита
            return value * (1.0 / (1UL << 53));
        }



        public static double[] NextDoubles(int count, bool secure = false)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var result = new double[count];

            if (!secure)
            {
                var rnd = Random.Shared;
                for (int i = 0; i < count; i++)
                    result[i] = rnd.NextDouble();

                return result;
            }

            for (int i = 0; i < count; i++)
            {
                ulong value = NextUInt64Secure() >> 11;
                result[i] = value * (1.0 / (1UL << 53));
            }

            return result;
        }


        public static long NextLong(long minInclusive, long maxExclusive, bool secure = false)
        {
            if (minInclusive >= maxExclusive)
                throw new ArgumentOutOfRangeException(nameof(minInclusive));

            if (!secure)
            {
                // Random.Shared не имеет NextLong → делаем через ulong
                ulong range = (ulong)(maxExclusive - minInclusive);
                ulong value = (ulong)Random.Shared.NextInt64(0, (long)range);
                return (long)(minInclusive + (long)value);
            }

            // secure вариант без bias
            ulong uRange = (ulong)(maxExclusive - minInclusive);

            ulong rand = NextUInt64Secure();
            ulong result = rand % uRange; // для большинства задач ок

            return (long)(minInclusive + (long)result);
        }


        public static long[] NextLongs(int count, long minInclusive, long maxExclusive, bool secure = false)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (minInclusive >= maxExclusive)
                throw new ArgumentOutOfRangeException(nameof(minInclusive));

            var result = new long[count];

            if (!secure)
            {
                ulong range = (ulong)(maxExclusive - minInclusive);

                for (int i = 0; i < count; i++)
                {
                    ulong value = (ulong)Random.Shared.NextInt64(0, (long)range);
                    result[i] = minInclusive + (long)value;
                }

                return result;
            }

            ulong uRange = (ulong)(maxExclusive - minInclusive);

            for (int i = 0; i < count; i++)
            {
                ulong rand = NextUInt64Secure();
                result[i] = minInclusive + (long)(rand % uRange);
            }

            return result;
        }



        public static T NextEnum<T>(bool secure = false) where T : struct, Enum
        {
            var values = Enum.GetValues<T>();

            int index = secure
                ? RandomNumberGenerator.GetInt32(values.Length)
                : Random.Shared.Next(values.Length);

            return values[index];
        }



        public static DateTime NextDateTime(DateTime from, DateTime to, bool secure = false)
        {
            if (from >= to)
                throw new ArgumentException("Invalid range");

            long range = to.Ticks - from.Ticks;

            long offset = secure
                ? NextLong(0, range, true)
                : NextLong(0, range, false);

            return new DateTime(from.Ticks + offset, from.Kind);


        }



        public static Guid NextGuid(bool secure = false)
        {
            if (!secure)
                return Guid.NewGuid(); //.CreateVersion7();

            Span<byte> bytes = stackalloc byte[16];
            RandomNumberGenerator.Fill(bytes);
            return new Guid(bytes);
        }



        public static string NextHexString(int byteLength, bool secure = false)
        {
            var bytes = NextBytes(byteLength, secure);
            return Convert.ToHexString(bytes);
        }




        public static string NextBase64(int byteLength, bool secure = false)
        {
            var bytes = NextBytes(byteLength, secure);
            return Convert.ToBase64String(bytes);
        }



        public static T Sample<T>(IReadOnlyList<T> source, bool secure = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.Count == 0)
                throw new ArgumentException("Source cannot be empty", nameof(source));

            int index = secure
                ? RandomNumberGenerator.GetInt32(source.Count)
                : Random.Shared.Next(source.Count);

            return source[index];
        }




        public static List<T> PickMany<T>(IList<T> source, int count, bool secure = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (count < 0 || count > source.Count)
                throw new ArgumentOutOfRangeException(nameof(count));

            var result = new List<T>(count);
            var buffer = new T[source.Count];
            source.CopyTo(buffer, 0);

            for (int i = 0; i < count; i++)
            {
                int j = secure
                    ? RandomNumberGenerator.GetInt32(i, buffer.Length)
                    : Random.Shared.Next(i, buffer.Length);

                (buffer[i], buffer[j]) = (buffer[j], buffer[i]);
                result.Add(buffer[i]);
            }

            return result;
        }


        public static string NextPassword(
            int length = 12,
            bool requireUpper = true,
            bool requireLower = true,
            bool requireDigit = true,
            bool requireSpecial = true,
            bool secure = true)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()-_=+[]{}<>?";

            string all = "";

            if (requireUpper) all += upper;
            if (requireLower) all += lower;
            if (requireDigit) all += digits;
            if (requireSpecial) all += special;

            if (string.IsNullOrEmpty(all))
                throw new ArgumentException("At least one character set must be enabled");

            if (length < (requireUpper ? 1 : 0) +
                         (requireLower ? 1 : 0) +
                         (requireDigit ? 1 : 0) +
                         (requireSpecial ? 1 : 0))
                throw new ArgumentException("Length too small for required constraints");

            var result = new char[length];
            int index = 0;

            // --- гарантируем наличие символов ---
            if (requireUpper) result[index++] = NextChar(upper, secure);
            if (requireLower) result[index++] = NextChar(lower, secure);
            if (requireDigit) result[index++] = NextChar(digits, secure);
            if (requireSpecial) result[index++] = NextChar(special, secure);

            // --- заполняем остальное ---
            for (; index < length; index++)
            {
                result[index] = NextChar(all, secure);
            }

            // --- перемешиваем (Fisher-Yates) ---
            Shuffle<char>(result, secure);

            return new string(result);
        }



        //public static void Shuffle(char[] array, bool secure)
        //{
        //    for (int i = array.Length - 1; i > 0; i--)
        //    {
        //        int j = secure
        //            ? RandomNumberGenerator.GetInt32(i + 1)
        //            : Random.Shared.Next(i + 1);

        //        (array[i], array[j]) = (array[j], array[i]);
        //    }
        //}


        public static void Shuffle<T>(T[] array, bool secure = false)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = secure
                    ? RandomNumberGenerator.GetInt32(i + 1)
                    : Random.Shared.Next(i + 1);

                (array[i], array[j]) = (array[j], array[i]);
            }
        }


        // --- Вспомогательный метод (secure ulong) ---
        public static ulong NextUInt64Secure()
        {
            Span<byte> bytes = stackalloc byte[8];
            RandomNumberGenerator.Fill(bytes);
            return BitConverter.ToUInt64(bytes);
        }
    }
}
