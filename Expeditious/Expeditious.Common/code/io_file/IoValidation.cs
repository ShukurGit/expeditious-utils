

namespace Expeditious.Common
{
    public static class IoValidation
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



        public static string ToSafeFileName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var invalidChars = Path.GetInvalidFileNameChars();
            var cleaned = new string(input
                .Select(c => invalidChars.Contains(c) ? '_' : c)
                .ToArray());

            // убираем точки и пробелы в конце
            cleaned = cleaned.TrimEnd('.', ' ');

            // защита от зарезервированных имён
            string upper = cleaned.ToUpperInvariant();
            string[] reserved = { "CON", "PRN", "AUX", "NUL",
                          "COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9",
                          "LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9"};

            if (reserved.Contains(upper))
                cleaned = "_" + cleaned;

            return cleaned;
        }


        public static string ToSafeFilePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return path;

            var parts = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = ToSafeFileName(parts[i]);
            }

            return string.Join(Path.DirectorySeparatorChar, parts);
        }
    }
}
