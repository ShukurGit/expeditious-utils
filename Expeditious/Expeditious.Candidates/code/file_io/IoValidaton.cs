


namespace Expedite.Utils.FileIO
{
    static public class IoValidaton
    {
        public static (bool isSuccess, string message, DirectoryInfo? directoryInfo) ValidateOrCreateDirectory(string dirPath)
        {
            if (string.IsNullOrWhiteSpace(dirPath))
                return (false, $"ERROR: Folder ″{dirPath}″ is null or empty.", null);

            try
            {
                DirectoryInfo di = Directory.CreateDirectory(dirPath);
                return (true, $"OK: Folder ″{dirPath}″ exists or was created.", di);
            }
            catch (Exception ex)
            {
                return (false, $"ERROR: Failed to create folder ″{dirPath}″. {ex.Message}", null);
            }
        }
    }
}
