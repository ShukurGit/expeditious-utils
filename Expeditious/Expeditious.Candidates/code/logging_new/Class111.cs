using System;
using System.Collections.Generic;
using System.Text;

namespace Expeditious.Candidates.code.logging_new
{
    internal class Class111
    {
        static public TimeZoneInfo TimeZoneInfoAze
        {
            get
            {
                return OperatingSystem.IsWindows()
                    ? TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time")
                    : TimeZoneInfo.FindSystemTimeZoneById("Asia/Baku");
            }
        }


        static public DateTimeOffset NowAz()
        {
            // get safely azeri time everywhere
            return TimeZoneInfo.ConvertTime((DateTimeOffset)DateTimeOffset.Now, OperatingSystem.IsWindows()
                    ? TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time")
                    : TimeZoneInfo.FindSystemTimeZoneById("Asia/Baku"));
        }
    }


}
