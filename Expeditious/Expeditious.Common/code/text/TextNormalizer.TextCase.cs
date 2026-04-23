

namespace Expeditious.Common
{
    public static partial class TextNormalizer
    {
        public static class TextCase
        {
            static public string? ToLowerAz(string? text)
            {
                return string.IsNullOrWhiteSpace(text) ? text : text.ToLower(System.Globalization.CultureInfo.GetCultureInfo("az-Latn-AZ"));
            }


            static public string? ToUpperAz(string? text)
            {
                return string.IsNullOrWhiteSpace(text) ? text : text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("az-Latn-AZ"));
            }
        }
    }
}
