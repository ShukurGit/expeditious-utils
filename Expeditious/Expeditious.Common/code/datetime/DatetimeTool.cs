

namespace Expeditious.Common
{
    public class DatetimeTool
    {
        public const String FORMAT_DATETIME_EXACT = "dd.MM.yyyy HH:mm:ss.fffff zzz";
        public const String FORMAT_DATETIME_DEFAULT_AZ = "dd.MM.yyyy HH:mm:ss.fff zzz";
        public const String FORMAT_DATETIME_WIN_STANDARD_AZ = "dd.MM.yyyy HH:mm:ss";
        public const String FORMAT_DATETIME_SQLITE = "yyyy-MM-dd HH:mm:ss.fff";
        public const String FORMAT_DATETIME_SORTABLE = "yyyy.MM.dd HH:mm:ss.fffff zzz";

        public const String FORMAT_DATE_SHORT_AZ = "dd.MM.yyyy";
        public const String FORMAT_TIME_SHORT_AZ = "HH:mm:ss";
        public const String FORMAT_DATETIME_TEXT = "dddd, dd MMMM yyyy HH:mm:ss";
        public const String FORMAT_DATETIME_DIGITS_INT64 = "yyyyMMddHHmmssfffff";


        static public readonly List<String> MONTH_AZE = new List<string>() { "Yanvar", "Fevral", "Mart", "Aprel", "May", "İyun", "İyul", "Avqust", "Sentyabr", "Oktyabr", "Noyabr", "Dekabr" };
        static public readonly List<String> MONTH_RUS = new List<string>() { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        static public readonly List<String> MONTH_ENG = new List<string>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        static public readonly List<String> WEEKDAY_AZE = new List<String>() { "Bazar ertəsi", "Çərşənbə axşamı", "Çərşənbə", "Cümə axşamı", "Cümə", "Şənbə", "Bazar" };
        static public readonly List<String> WEEKDAY_RUS = new List<String>() { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
        static public readonly List<String> WEEKDAY_ENG = new List<String>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        //static public readonly IFormatProvider CULTURE_EnUS = new System.Globalization.CultureInfo("en-US");
        //static public readonly IFormatProvider CULTURE_AzLat = new System.Globalization.CultureInfo("az-Latn-AZ");
        //static public readonly IFormatProvider CULTURE_AzCyr = new System.Globalization.CultureInfo("az-Cyrl-AZ");
        //static public readonly IFormatProvider CULTURE_Ru = new System.Globalization.CultureInfo("ru-RU");
        //static public readonly IFormatProvider CULTURE_Invariant = System.Globalization.CultureInfo.InvariantCulture;

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
            return TimeZoneInfo.ConvertTime((DateTimeOffset)DateTimeOffset.Now, TimeZoneInfoAze);
        }


        static public DateTimeOffset? ConvertToAzeTime(DateTimeOffset? value)
        {
            if (value == null) return null;
            return TimeZoneInfo.ConvertTime((DateTimeOffset)value, TimeZoneInfoAze);
        }


        //  15.04.2026 18:18:52     (eq ToStr(value, format: FORMAT_DATETIME_WIN_STANDARD_AZ))
        static public String ToStrAzWindowsStandard(DateTimeOffset? value)
        {
            if (value == null) return "";
            DateTimeOffset localDTO = ((DateTimeOffset)value).ToLocalTime();
            return localDTO.ToString("G", SharedConstCultures.CULTURE_AzLat);
        }

        static public String ToStr(DateTimeOffset? value, String format = FORMAT_DATETIME_DEFAULT_AZ)
        {
            if (value == null) return "";
            DateTimeOffset localDTO = ((DateTimeOffset)value).ToLocalTime();
            return localDTO.ToString(format);
        }


        static public DateTimeOffset? ParseStr(String strDateTimeOffset, String format = FORMAT_DATETIME_DEFAULT_AZ)
        {
            if (string.IsNullOrWhiteSpace(strDateTimeOffset)) return null;
            if (strDateTimeOffset.Trim().ToLower() == "null") return null;

            try
            {
                return DateTimeOffset.ParseExact(strDateTimeOffset, format, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                // return null;
                throw new Exception($"ERROR: Cant parse DateTimeOffset from `{strDateTimeOffset}`. {ex.Message}");
            }
        }


        static public long ToInt64(DateTimeOffset? dateTimeOffset)
        {
            if (dateTimeOffset == null) return 0;
            DateTimeOffset localDTO = ((DateTimeOffset)dateTimeOffset).ToLocalTime();
            return Convert.ToInt64(DatetimeTool.ToStr(localDTO, DatetimeTool.FORMAT_DATETIME_DIGITS_INT64));
        }


        static public DateTimeOffset ParseUnixTimeSeconds(Int64 unixTimeSeconds)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds);
        }


        static public DateTimeOffset ParseUnixTimeMilliseconds(Int64 unixTimeMilliseconds)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeMilliseconds);
        }


        static public DateTimeOffset ParseTicks(Int64 ticks, Boolean isUtcTime = true)
        {
            // Important: In any case, you must first convert it to the UTS format
            DateTimeOffset utcDto = new DateTimeOffset(ticks, TimeSpan.Zero);
            DateTimeOffset localDto = utcDto.ToOffset(TimeSpan.FromHours(4));

            return isUtcTime ? localDto : utcDto;
        }




        static public String ToTextDateAz(DateTimeOffset dto)
        {
            return ToTextDateAz(dto.LocalDateTime);
        }


        static public String ToTextDateAz(DateTime dt)
        {
            List<String> MONTH_AZE = new List<string>() { "Yanvar", "Fevral", "Mart", "Aprel", "May", "İyun", "İyul", "Avqust", "Sentyabr", "Oktyabr", "Noyabr", "Dekabr" };
            String day = dt.Day.ToString();
            String year = dt.Year.ToString();
            String month = MONTH_AZE[dt.Month - 1];

            int lastDigit = Convert.ToInt32(year.Substring(3, 1));
            int preLastDigit = Convert.ToInt32(year.Substring(2, 1));
            int secondDigit = Convert.ToInt32(year.Substring(1, 1));
            int firstDigit = Convert.ToInt32(year.Substring(0, 1));

            String suffix = "";

            if (lastDigit == 1 || lastDigit == 2 || lastDigit == 5 || lastDigit == 7 || lastDigit == 8)
            {
                suffix = "-ci";
            }
            else if (lastDigit == 3 || lastDigit == 4)
            {
                suffix = "-cü";
            }
            else if (lastDigit == 9)
            {
                suffix = "-cu";
            }
            else if (lastDigit == 6)
            {
                suffix = "-cı";
            }
            else if (lastDigit == 0)
            {
                if (preLastDigit == 1 || preLastDigit == 3)
                {
                    suffix = "-cu";
                }
                else if (preLastDigit == 2 || preLastDigit == 5 || preLastDigit == 7 || preLastDigit == 8)
                {
                    suffix = "-ci";
                }
                else if (preLastDigit == 4 || preLastDigit == 6 || preLastDigit == 9)
                {
                    suffix = "-cı";
                }
                else if (preLastDigit == 0)
                {
                    if (secondDigit != 0)
                    {
                        suffix = "-cü";
                    }
                    else
                    {
                        suffix = "-ci";
                    }
                }
            }

            return $"{day} {month} {year}{suffix} il";
        }



        //  Конвертирует DateTime в форматированную строку на азерб языке. Например: "21 yanvar 2011, 12:05:15.265"
        static public String ToAzeriString(DateTime dt)
        {
            String month = MONTH_AZE[dt.Month];
            return String.Format("{0} {1} {2}, {3:D2}:{4:D2}:{5:D2}.{6:D3}", dt.Day, month, dt.Year, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }



        // Конвертирует форматированную строку на азерб языке типа "21 yanvar 2011, 12:05:15.265" в формат DateTime
        static public DateTime FromAzeriString(String azFormattedDateTime)
        {
            Int32 day, month, year, hour, minute, second, millisecond;

            String[] splittedString = azFormattedDateTime.Trim().Split(' ');
            day = Int32.Parse(splittedString[0]);

            month = MONTH_AZE.IndexOf(splittedString[1].Trim());

            splittedString[2] = splittedString[2].Replace(',', ' ');
            year = Int32.Parse(splittedString[2]);

            String[] splittedFromMilliseconds = splittedString[3].Split('.');
            millisecond = Int32.Parse(splittedFromMilliseconds[1]);

            String[] splittedTime = splittedFromMilliseconds[0].Split(':');
            hour = Int32.Parse(splittedTime[0]);
            minute = Int32.Parse(splittedTime[1]);
            second = Int32.Parse(splittedTime[2]);

            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }
    }
}








/*
 //  www.csharp-examples.net/string-format-datetime/

// create date time 2008-03-09 16:05:07.123
DateTime dt = new DateTime(2008, 3, 9, 16, 5, 7, 123);

String.Format("{0:y yy yyy yyyy}", dt);  // "8 08 008 2008"   year
String.Format("{0:M MM MMM MMMM}", dt);  // "3 03 Mar March"  month
String.Format("{0:d dd ddd dddd}", dt);  // "9 09 Sun Sunday" day
String.Format("{0:h hh H HH}",     dt);  // "4 04 16 16"      hour 12/24
String.Format("{0:m mm}",          dt);  // "5 05"            minute
String.Format("{0:s ss}",          dt);  // "7 07"            second
String.Format("{0:f ff fff ffff}", dt);  // "1 12 123 1230"   sec.fraction
String.Format("{0:F FF FFF FFFF}", dt);  // "1 12 123 123"    without zeroes
String.Format("{0:t tt}",          dt);  // "P PM"            A.M. or P.M.
String.Format("{0:z zz zzz}",      dt);  // "-6 -06 -06:00"   time zone





// date separator in german culture is "." (so "/" changes to ".")
String.Format("{0:d/M/yyyy HH:mm:ss}", dt); // "9/3/2008 16:05:07" - english (en-US)
String.Format("{0:d/M/yyyy HH:mm:ss}", dt); // "9.3.2008 16:05:07" - german (de-DE)






// month/day numbers without/with leading zeroes
String.Format("{0:M/d/yyyy}", dt);            // "3/9/2008"
String.Format("{0:MM/dd/yyyy}", dt);          // "03/09/2008"

// day/month names
String.Format("{0:ddd, MMM d, yyyy}", dt);    // "Sun, Mar 9, 2008"
String.Format("{0:dddd, MMMM d, yyyy}", dt);  // "Sunday, March 9, 2008"

// two/four digit year
String.Format("{0:MM/dd/yy}", dt);            // "03/09/08"
String.Format("{0:MM/dd/yyyy}", dt);          // "03/09/2008"


 


Specifier	DateTimeFormatInfo property	Pattern value (for en-US culture)
t	ShortTimePattern	h:mm tt
d	ShortDatePattern	M/d/yyyy
T	LongTimePattern	h:mm:ss tt
D	LongDatePattern	dddd, MMMM dd, yyyy
f	(combination of D and t)	dddd, MMMM dd, yyyy h:mm tt
F	FullDateTimePattern	dddd, MMMM dd, yyyy h:mm:ss tt
g	(combination of d and t)	M/d/yyyy h:mm tt
G	(combination of d and T)	M/d/yyyy h:mm:ss tt
m, M	MonthDayPattern	MMMM dd
y, Y	YearMonthPattern	MMMM, yyyy
r, R	RFC1123Pattern	ddd, dd MMM yyyy HH':'mm':'ss 'GMT' (*)
s	SortableDateTi­mePattern	yyyy'-'MM'-'dd'T'HH':'mm':'ss (*)
u	UniversalSorta­bleDateTimePat­tern	yyyy'-'MM'-'dd HH':'mm':'ss'Z' (*)
 	 	(*) = culture independent





String.Format("{0:t}", dt);  // "4:05 PM"                         ShortTime
String.Format("{0:d}", dt);  // "3/9/2008"                        ShortDate
String.Format("{0:T}", dt);  // "4:05:07 PM"                      LongTime
String.Format("{0:D}", dt);  // "Sunday, March 09, 2008"          LongDate
String.Format("{0:f}", dt);  // "Sunday, March 09, 2008 4:05 PM"  LongDate+ShortTime
String.Format("{0:F}", dt);  // "Sunday, March 09, 2008 4:05:07 PM" FullDateTime
String.Format("{0:g}", dt);  // "3/9/2008 4:05 PM"                ShortDate+ShortTime
String.Format("{0:G}", dt);  // "3/9/2008 4:05:07 PM"             ShortDate+LongTime
String.Format("{0:m}", dt);  // "March 09"                        MonthDay
String.Format("{0:y}", dt);  // "March, 2008"                     YearMonth
String.Format("{0:r}", dt);  // "Sun, 09 Mar 2008 16:05:07 GMT"   RFC1123
String.Format("{0:s}", dt);  // "2008-03-09T16:05:07"             SortableDateTime
String.Format("{0:u}", dt);  // "2008-03-09 16:05:07Z"            UniversalSortableDateTime







*/
