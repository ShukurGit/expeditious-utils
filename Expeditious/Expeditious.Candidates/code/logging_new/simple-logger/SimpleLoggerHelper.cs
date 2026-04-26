using System;
using System.Collections.Generic;
using System.Text;

namespace Expeditious.Candidates.SimpleLogger_a2
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
