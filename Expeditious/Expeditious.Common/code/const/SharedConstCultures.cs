

namespace Expeditious.Common
{
    public static class SharedConstCultures
    {
        static public readonly IFormatProvider CULTURE_EnUS = System.Globalization.CultureInfo.GetCultureInfo("en-US");
        static public readonly IFormatProvider CULTURE_AzLat = System.Globalization.CultureInfo.GetCultureInfo("az-Latn-AZ"); // new System.Globalization.CultureInfo("az-Latn-AZ");
        static public readonly IFormatProvider CULTURE_AzCyr = System.Globalization.CultureInfo.GetCultureInfo("az-Cyrl-AZ");
        static public readonly IFormatProvider CULTURE_Ru = System.Globalization.CultureInfo.GetCultureInfo("ru-RU");
        static public readonly IFormatProvider CULTURE_Invariant = System.Globalization.CultureInfo.InvariantCulture;
    }
}
