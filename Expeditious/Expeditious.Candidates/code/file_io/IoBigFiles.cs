


namespace Expedite.Utils.FileIO
{
    static public class IoBigFiles
    {
        static public async Task CopyFileAsync(string sourceFilePath, string destFilePath)
        {
            await using var source = new FileStream(
                sourceFilePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 65536,
                useAsync: true);

            await using var dest = new FileStream(
                destFilePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 65536,
                useAsync: true);

            await source.CopyToAsync(dest);
        }



        static public async Task CopyModifiedFileAsync(string sourceFilePath, string destFilePath)
        {
            

            await using var source = new FileStream(
                sourceFilePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                65536,
                true);

            await using var dest = new FileStream(
                destFilePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                65536,
                true);

            using var reader = new StreamReader(source);
            await using var writer = new StreamWriter(dest);

            string? line;

            // int count = 0;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                //if (++count % 1000 == 0)
                //    ShowThreads();


                // some editing
                var newLine = line;
                Console.WriteLine(newLine);

                await writer.WriteLineAsync(newLine);
            }
        }


        //static public void ShowThreads()
        //{
        //    int workerThreads = 0, completionPortThreads = 0;
        //    ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
        //    Console.WriteLine("{0} thead	{1} cport threads", workerThreads, completionPortThreads);
        //}
    }
}
