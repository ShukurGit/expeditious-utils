using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Expeditious.Common
{
    public static class TextTransliterator
    {

        #region _  private  _

        private static readonly string ALPHABET_AZE_LAT = SharedConstText.AlphabetAzeLat;


        private const string MAPPED_AZE_CYR = "АБҸЧДЕӘФҜҒҺХЫИЖКГЛМНОӨПРСШТУҮВЈЗабҹчдеәфҝғһхыижкглмноөпрсштуүвјз";
        private const string MAPPED_AZE_OLD_KBD_1 = "AБЪЧДЕЯФЭЬЩХЫИЖКГЛМНОЮПРСШТУЦВЙЗабъчдеяфэьщхыижкглмноюпрсштуцвйз";
        private const string MAPPED_AZE_OLD_KBD_2 = "AБЖЧДЕЯФЭЬЩХЫИЪКГЛМНОЮПРСШТУЦВЙЗабжчдеяфэьщхыиъкглмноюпрсштуцвйз";
        private const string MAPPED_AZE_ASCII_CHAR = "ABCCDEEFGGHXIIJKQLMNOOPRSSTUUVYZabccdeefgghxiijkqlmnooprsstuuvyz";
        private static readonly string[] MAPPED_AZE_ASCII_STR = ["A", "B", "C", "Ch", "D", "E", "E", "F", "G", "G", "H", "Kh", "I", "I", "Zj", "K", "G", "L", "M", "N", "O", "O", "P", "R", "S", "Sh", "T", "U", "U", "V", "Y", "Z", "a", "b", "c", "ch", "d", "e", "e", "f", "g", "g", "h", "kh", "i", "i", "zj", "k", "g", "l", "m", "n", "o", "o", "p", "r", "s", "sh", "t", "u", "u", "v", "y", "z",];
        private static readonly string[] MAPPED_AZE_RUSIFIED_STR = ["А", "Б", "Дж", "Ч", "Д", "Э", "Е", "Ф", "Г", "Г", "Х", "Х", "Ы", "И", "Ж", "К", "Г", "Л", "М", "Н", "О", "О", "П", "Р", "С", "Ш", "Т", "У", "У", "В", "Й", "З", "а", "б", "дж", "ч", "д", "е", "е", "ф", "г", "г", "х", "х", "ы", "и", "ж", "к", "г", "л", "м", "н", "о", "о", "п", "р", "с", "ш", "т", "у", "ю", "в", "й", "з",];


        private static ReadOnlyDictionary<T, K> CreateReadOnlyDictionary<T, K>(T[] keys, K[] values) where T : notnull where K : notnull //where T: struct where K : struct
        {
            Dictionary<T, K> result = new Dictionary<T, K>();

            for (int i = 0; i < keys.Length; i++)
                result.Add(keys[i], values[i]);

            return result.AsReadOnly();
        }




        private static ReadOnlyDictionary<char, char> GetMapAzeLatToCyr()
        {
            return CreateReadOnlyDictionary<char, char>(ALPHABET_AZE_LAT.ToCharArray(), MAPPED_AZE_CYR.ToCharArray());
        }

        private static ReadOnlyDictionary<char, char> GetMapAzeCyrToLat()
        {
            return CreateReadOnlyDictionary<char, char>(MAPPED_AZE_CYR.ToCharArray(), ALPHABET_AZE_LAT.ToCharArray());
        }

        private static ReadOnlyDictionary<char, char> GetMapAzeLatToOldKbd1()
        {
            return CreateReadOnlyDictionary<char, char>(ALPHABET_AZE_LAT.ToCharArray(), MAPPED_AZE_OLD_KBD_1.ToCharArray());
        }

        private static ReadOnlyDictionary<char, char> GetMapAzeOldKbd1ToLat()
        {
            return CreateReadOnlyDictionary<char, char>(MAPPED_AZE_OLD_KBD_1.ToCharArray(), ALPHABET_AZE_LAT.ToCharArray());
        }

        private static ReadOnlyDictionary<char, char> GetMapAzeLatToOldKbd2()
        {
            return CreateReadOnlyDictionary<char, char>(ALPHABET_AZE_LAT.ToCharArray(), MAPPED_AZE_OLD_KBD_2.ToCharArray());
        }

        private static ReadOnlyDictionary<char, char> GetMapAzeOldKbd2ToLat()
        {
            return CreateReadOnlyDictionary<char, char>(MAPPED_AZE_OLD_KBD_2.ToCharArray(), ALPHABET_AZE_LAT.ToCharArray());
        }

        private static ReadOnlyDictionary<char, char> GetMapAzeLatToAsciiChar()
        {
            return CreateReadOnlyDictionary<char, char>(ALPHABET_AZE_LAT.ToCharArray(), MAPPED_AZE_ASCII_CHAR.ToCharArray());
        }

        private static ReadOnlyDictionary<char, string> GetMapAzeLatToAsciiStr()
        {
            return CreateReadOnlyDictionary<char, string>(ALPHABET_AZE_LAT.ToCharArray(), MAPPED_AZE_ASCII_STR);
        }

        private static ReadOnlyDictionary<char, string> GetMapAzeLatToRusified()
        {
            return CreateReadOnlyDictionary<char, string>(ALPHABET_AZE_LAT.ToCharArray(), MAPPED_AZE_RUSIFIED_STR);
        }




        #endregion _  private  _



        public static ReadOnlyDictionary<char, char> DictionaryMapAzeLatToCyr { get { return GetMapAzeLatToCyr(); } }
        public static ReadOnlyDictionary<char, char> DictionaryMapAzeCyrToLat { get { return GetMapAzeCyrToLat(); } }
        public static ReadOnlyDictionary<char, char> DictionaryMapAzeLatToOldKbd1 { get { return GetMapAzeLatToOldKbd1(); } }
        public static ReadOnlyDictionary<char, char> DictionaryMapAzeOldKbd1ToLat { get { return GetMapAzeOldKbd1ToLat(); } }
        public static ReadOnlyDictionary<char, char> DictionaryMapAzeLatToOldKbd2 { get { return GetMapAzeLatToOldKbd2(); } }
        public static ReadOnlyDictionary<char, char> DictionaryMapAzeOldKbd2ToLat { get { return GetMapAzeOldKbd2ToLat(); } }
        public static ReadOnlyDictionary<char, char> DictionaryMapAzeLatToAsciiChar { get { return GetMapAzeLatToAsciiChar(); } }
        public static ReadOnlyDictionary<char, string> DictionaryMapAzeLatToAsciiStr { get { return GetMapAzeLatToAsciiStr(); } }
        public static ReadOnlyDictionary<char, string> DictionaryMapAzeLatToRusified { get { return GetMapAzeLatToRusified(); } }




        public static string ConvertAzLatToAzCyr(string iniText)
        {
            return TextModifier.ReplaceMultipleChars(iniText, DictionaryMapAzeLatToCyr);
        }


        public static string ConvertAzCyrToAzLat(string iniText)
        {
            return TextModifier.ReplaceMultipleChars(iniText, DictionaryMapAzeCyrToLat);
        }


        public static string ConvertAzLatToAzOldKbd1(string iniText)
        {
            return TextModifier.ReplaceMultipleChars(iniText, DictionaryMapAzeLatToOldKbd1);
        }


        public static string ConvertAzOldKbd1ToAzLat(string iniText)
        {
            return TextModifier.ReplaceMultipleChars(iniText, DictionaryMapAzeOldKbd1ToLat);
        }


        public static string ConvertAzLatToAzOldKbd2(string iniText)
        {
            return TextModifier.ReplaceMultipleChars(iniText, DictionaryMapAzeLatToOldKbd2);
        }


        public static string ConvertAzOldKbd2ToAzLat(string iniText)
        {
            return TextModifier.ReplaceMultipleChars(iniText, DictionaryMapAzeOldKbd2ToLat);
        }


        public static string ConvertAzLatToAsciiChar(string iniText)
        {
            return TextModifier.ReplaceMultipleChars(iniText, DictionaryMapAzeLatToAsciiChar);
        }


        public static string ConvertAzLatToAsciiStr(string iniText)
        {
            return TextModifier.ReplaceMultipleChars(iniText, DictionaryMapAzeLatToAsciiStr);
        }


        public static string ConvertAzLatToRusified(string iniText)
        {
            return TextModifier.ReplaceMultipleChars(iniText, DictionaryMapAzeLatToRusified);
        }
    }
}
