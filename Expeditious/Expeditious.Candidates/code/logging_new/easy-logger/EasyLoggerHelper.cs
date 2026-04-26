

namespace Expeditious.Candidates
{
    public static class EasyLoggerHelper
    {
        static public DateTime TimestampAz()
        {
            // get safely azeri time everywhere
            return TimeZoneInfo.ConvertTime((DateTimeOffset)DateTimeOffset.Now, OperatingSystem.IsWindows()
                    ? TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time")
                    : TimeZoneInfo.FindSystemTimeZoneById("Asia/Baku")).DateTime;
        }
    }
}
