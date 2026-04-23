

namespace Expeditious.Common
{
    public static class SharedConstDate
    {


        public enum MonthNames
        {
            January = 1, February = 2, March = 3, April = 4, May = 5, June = 6, July = 7, August = 8, September = 9, October = 10, November = 11, December = 12
        }

        static public readonly List<String> MONTH_AZE = new List<string>() { "Yanvar", "Fevral", "Mart", "Aprel", "May", "İyun", "İyul", "Avqust", "Sentyabr", "Oktyabr", "Noyabr", "Dekabr" };
        static public readonly List<String> MONTH_RUS = new List<string>() { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        static public readonly List<String> MONTH_ENG = new List<string>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        static public readonly List<String> WEEKDAY_AZE = new List<string>() { "Bazar ertəsi", "Çərşənbə axşamı", "Çərşənbə", "Cümə axşamı", "Cümə", "Şənbə", "Bazar" };
        static public readonly List<String> WEEKDAY_RUS = new List<string>() { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
        static public readonly List<String> WEEKDAY_ENG = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        static public readonly String FORMAT_DATETIME_DEFAULT = "yyyy-MM-dd HH:mm:ss.fff";
        static public readonly String FORMAT_DATE_DEFAULT = "yyyy-MM-dd";
        static public readonly String FORMAT_TIME_DEFAULT = "HH:mm:ss.fff";

        static public readonly String FORMAT_DATETIME_DIGITS = "yyyyMMddHHmmssfff";

        static public readonly String FORMAT_TIME_EXTENDED = "HH:mm:ss.fff";
        static public readonly String FORMAT_TIME_SHORT = "HH:mm:ss";

        static public readonly String FORMAT_TIME_EXTENDED_DIGITS = "HHmmssfff";
        static public readonly String FORMAT_TIME_SHORT_DIGITS = "HHmmss";

        static public readonly String FORMAT_TIME_CUSTOM_INT32 = "HHmmss";
        static public readonly String FORMAT_DATETIME_CUSTOM_INT64 = "yyyyMMddHHmmssfff";
    }
}