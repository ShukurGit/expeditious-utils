
using System.Text;


public static partial class TextTransformer
{
    static public class Base64Text
    {
        //// Base64FormattingOptions.InsertLineBreaks - Inserts line breaks after every 76 characters in the string representation
        //static public String To(String text, Base64FormattingOptions base64FormattingOptions = Base64FormattingOptions.InsertLineBreaks)
        //{
        //    return Convert.ToBase64String(Encoding.UTF8.GetBytes(text), base64FormattingOptions);
        //}

        //static public String From(String text)
        //{
        //    return Encoding.UTF8.GetString(Convert.FromBase64String(text));
        //}

        // Base64FormattingOptions.InsertLineBreaks - Inserts line breaks after every 76 characters in the string representation
        public static string To(string iniText, Base64FormattingOptions base64FormattingOptions = Base64FormattingOptions.InsertLineBreaks)
        {
            Byte[] bytesUtf8 = Encoding.UTF8.GetBytes(iniText);
            return Convert.ToBase64String(bytesUtf8, base64FormattingOptions);
        }



        public static string To(string iniText, bool withLineBreak = true)
        {
            Base64FormattingOptions base64FormattingOptions = withLineBreak ? Base64FormattingOptions.InsertLineBreaks : Base64FormattingOptions.None;

            Byte[] bytesUtf8 = Encoding.UTF8.GetBytes(iniText);
            return Convert.ToBase64String(bytesUtf8, base64FormattingOptions);
        }


        static public String From(String base64text)
        {
            Byte[] bytes = Convert.FromBase64String(base64text);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
