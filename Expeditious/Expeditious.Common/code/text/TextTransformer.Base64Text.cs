
using System.Text;


public static partial class TextTransformer
{
    static public class Base64Text
    {
        static public String To(String text, Base64FormattingOptions base64FormattingOptions = Base64FormattingOptions.InsertLineBreaks)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text), base64FormattingOptions);
        }


        static public String From(String text)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(text));
        }
    }
}
