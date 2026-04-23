
using Expeditious.Common;


namespace Expeditious.UsefulExtensions.Datetime
{
    static public class ExtDatetime
    {

        static public string xToWindowsStandardStringAz(this DateTimeOffset value)
        {
            return DatetimeTool.ToStrAzWindowsStandard(value);
        }


        static public DateTimeOffset xToLocalTimeAze(this DateTimeOffset value)
        {
            return (DateTimeOffset)DatetimeTool.ConvertToAzeTime(value);
        }


        static public string xToPrettyString(this DateTimeOffset value)
        {
            return DatetimeTool.ToStr(value);
        }
    }
}
