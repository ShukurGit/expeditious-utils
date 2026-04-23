


namespace Expedite.Utils.FileIO
{
    static class IoCoreAsync
    {
        static public async Task<List<char>> ReadAllCharsAsync(string filePath)
        {
            var result = new List<char>();
            char[] buffer = new char[4096];

            using var reader = File.OpenText(filePath);

            int read;
            while ((read = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                for (int i = 0; i < read; i++)
                    result.Add(buffer[i]);
            }

            return result;
        }
    }
}
