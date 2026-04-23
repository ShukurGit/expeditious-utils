using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Expeditious.Common
{
    public static class SharedConstText
    {
        private const string ALPHABET_ENG = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const string ALPHABET_AZE = "ABCÇDEƏFGĞHXIİJKQLMNOÖPRSŞTUÜVYZabcçdeəfgğhxıijkqlmnoöprsştuüvyz";
        private const string ALPHABET_AZECYR = "АБВГҒДЕӘЖЗИЫЈКҜЛМНОӨПРСТУҮФХҺЧҸШабвгғдеәжзиыјкҝлмноөпрстуүфхһчҹш";
        private const string ALPHABET_RUS = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public const string DIGITS = "0123456789";

        //public const string ALPHABET_ENG = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        //public const string ALPHABET_ENG_BIG = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //public const string ALPHABET_ENG_SMALL = "abcdefghijklmnopqrstuvwxyz";

        //public const string ALPHABET_AZE = "ABCÇDEƏFGĞHXIİJKQLMNOÖPRSŞTUÜVYZabcçdeəfgğhxıijkqlmnoöprsştuüvyz";
        //public const string ALPHABET_AZE_BIG = "ABCÇDEƏFGĞHXIİJKQLMNOÖPRSŞTUÜVYZ";
        //public const string ALPHABET_AZE_SMALL = "abcçdeəfgğhxıijkqlmnoöprsştuüvyz";

        //public const string ALPHABET_RUS = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        //public const string ALPHABET_RUS_BIG = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        //public const string ALPHABET_RUS_SMALL = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        //public const string ALPHABET_AZECYR = "АБВГҒДЕӘЖЗИЫЈКҜЛМНОӨПРСТУҮФХҺЧҸШабвгғдеәжзиыјкҝлмноөпрстуүфхһчҹш";
        //public const string ALPHABET_AZECYR_BIG = "АБВГҒДЕӘЖЗИЫЈКҜЛМНОӨПРСТУҮФХҺЧҸШ";
        //public const string ALPHABET_AZECYR_SMALL = "абвгғдеәжзиыјкҝлмноөпрстуүфхһчҹш";

        //public const string ALPHABET_AZE_OLD_KBD_1 = "AБЪЧДЕЯФЭЬЩХЫИЖКГЛМНОЮПРСШТУЦВЙЗабъчдеяфэьщхыижкглмноюпрсштуцвйз";
        //public const string ALPHABET_AZE_OLD_KBD_1_BIG = "AБЪЧДЕЯФЭЬЩХЫИЖКГЛМНОЮПРСШТУЦВЙЗ";
        //public const string ALPHABET_AZE_OLD_KBD_1_SMALL = "абъчдеяфэьщхыижкглмноюпрсштуцвйз";

        //public const string ALPHABET_AZE_OLD_KBD_2 = "AБЖЧДЕЯФЭЬЩХЫИЪКГЛМНОЮПРСШТУЦВЙЗабжчдеяфэьщхыиъкглмноюпрсштуцвйз";
        //public const string ALPHABET_AZE_OLD_KBD_2_BIG = "AБЖЧДЕЯФЭЬЩХЫИЪКГЛМНОЮПРСШТУЦВЙЗ";
        //public const string ALPHABET_AZE_OLD_KBD_2_SMALL = "абжчдеяфэьщхыиъкглмноюпрсштуцвйз";



        public static string AlphabetEng { get { return ALPHABET_ENG; } }
        public static string AlphabetEngBig { get { return GetBigLetters(ALPHABET_ENG); } }
        public static string AlphabetEngSmall { get { return GetSmallLetters(ALPHABET_ENG); } }


        public static string AlphabetAzeLat { get { return ALPHABET_AZE; } }
        public static string AlphabetAzeLatBig { get { return GetBigLetters(ALPHABET_AZE); } }
        public static string AlphabetAzeLatSmall { get { return GetSmallLetters(ALPHABET_AZE); } }


        public static string AlphabetAzeCyr { get { return ALPHABET_AZECYR; } }
        public static string AlphabetAzeCyrBig { get { return GetBigLetters(ALPHABET_AZECYR); } }
        public static string AlphabetAzeCyrSmall { get { return GetSmallLetters(ALPHABET_AZECYR); } }



        public static string AlphabetRus { get { return ALPHABET_RUS; } }
        public static string AlphabetRusBig { get { return GetBigLetters(ALPHABET_RUS); } }
        public static string AlphabetRusSmall { get { return GetSmallLetters(ALPHABET_RUS); } }






        private static string GetBigLetters(string alphabet)
        {
            if (string.IsNullOrWhiteSpace(alphabet)) throw new Exception("aphabet not valid");
            return alphabet.Substring(0, alphabet.Length / 2);
        }


        private static string GetSmallLetters(string alphabet)
        {
            if (string.IsNullOrWhiteSpace(alphabet)) throw new Exception("aphabet not valid");
            return alphabet.Substring(alphabet.Length / 2, alphabet.Length / 2);
        }

    }
}
