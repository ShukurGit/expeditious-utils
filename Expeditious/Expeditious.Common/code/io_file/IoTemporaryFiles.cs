


namespace Expeditious.Common
{
    static public class IoTemporaryFiles
    {
        // C:\Users\user\AppData\Local\Temp\
        static public String GetWindowsTempFolder()
        {
            return System.IO.Path.GetTempPath();
        }


        // ny5lpu1s.swi, 0fkfgzeo.xdj
        static public String GetWindowsRandomFileName()
        {
            return Path.GetRandomFileName();
        }



        // C:\Users\user\AppData\Local\Temp\tmpyrh4eo.tmp
        static public String GetWindowsTempFilePath()
        {
            return Path.GetTempFileName();
        }


        static public string GetTempFilePath(string? tempFolder = null, string subjectPrefix = "TempFile", string fileExtention = "txt")
        {
            string datestamp = DateTime.Now.ToString("yyyy_MM_dd");
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_ffffff");
            string ticks = DateTime.UtcNow.Ticks.ToString();
            string guid = Guid.CreateVersion7().ToString("N");

            string uniqueMarker = timestamp; // timestamp, ticks, guid

            if (string.IsNullOrWhiteSpace(subjectPrefix))
                subjectPrefix = "TempFile";

            if (string.IsNullOrWhiteSpace(tempFolder))
                tempFolder = tempFolder ?? System.IO.Path.Combine(System.IO.Path.GetTempPath(), "TEMP_FILES", subjectPrefix, datestamp);

            Directory.CreateDirectory(tempFolder);


            if (string.IsNullOrWhiteSpace(fileExtention))
                subjectPrefix = "txt";

            String fileName = $"{subjectPrefix}_{uniqueMarker}.{fileExtention}";

            return System.IO.Path.Combine(tempFolder, fileName);
        }
    }
}
