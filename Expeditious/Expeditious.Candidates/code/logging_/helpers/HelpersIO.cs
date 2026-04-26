



namespace Yeni.YeniLogging
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;


    public class HelpersIO
    {
        public static Boolean CheckFolder(String folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }


        private String MakeValidFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }


        public static String ToSafeTextIoPath(String text, String replacementInvalids = "_", String replacementNullEmpty = "_")
        {
            if (String.IsNullOrWhiteSpace(text)) return replacementNullEmpty;

            foreach (char ch in Path.GetInvalidFileNameChars())
                text = text.Replace(ch.ToString(), replacementInvalids); // String.Empty);

            foreach (char ch in Path.GetInvalidPathChars())
                text = text.Replace(ch.ToString(), replacementInvalids); // String.Empty);

            // if text is empty after replacing invalid chars
            if (String.IsNullOrWhiteSpace(text)) return replacementNullEmpty;

            return (text);
        }


        public static String ToSafeTextDb(String txt)
        {
            txt = txt.Replace(ConstChars.UNSAFE_DOUBLECOMMA_34, ConstChars.SAFE_DOUBLECOMMA_8243);
            txt = txt.Replace(ConstChars.UNSAFE_APOSTROPHE_39, ConstChars.SAFE_APOSTROPHE_900);
            txt = txt.Replace(ConstChars.UNSAFE_COMMA_44, ConstChars.SAFE_COMMA_8218);

            return txt;
        }


        public String GetSafeLayerFileNameString(String name)
        {
            name = name.Replace('.', '_');
            name = name.Replace('+', '_');
            name = name.Replace('-', '_');
            name = name.Replace('!', '_');
            name = name.Replace('?', '_');
            name = name.Replace(' ', '_');
            name = name.Replace('*', '_');
            name = name.Replace('`', '_');
            name = name.Replace('~', '_');
            name = name.Replace('#', '_');
            name = name.Replace(')', '_');
            name = name.Replace('(', '_');
            name = name.Replace('&', '_');
            name = name.Replace('^', '_');
            name = name.Replace(':', '_');
            name = name.Replace(';', '_');
            name = name.Replace('%', '_');
            name = name.Replace('$', '_');
            name = name.Replace('@', '_');
            name = name.Replace('"', '_');
            name = name.Replace('\'', '_');

            if (!Char.IsLetter(name.First()))
                name = "a" + name;
            return name;
        }
    }
}
