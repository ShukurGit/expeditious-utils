



namespace Expeditious.Candidates
{
    using System.Text;



    static public class WebDataIo
    {
        static private readonly HttpClient _httpClient = new HttpClient();

        static WebDataIo()
        {
            // important: Many servers block without User-Agent
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120 Safari/537.36");

            _httpClient.DefaultRequestHeaders.Accept.ParseAdd("*/*");
        }

        static public async Task DownloadMediaAsync(string url, string resultFilePath, CancellationToken ct = default)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(resultFilePath)!);

            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            // You can override/add headers at the request level
            request.Headers.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120 Safari/537.36");


            using var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, ct);

            response.EnsureSuccessStatusCode();

            Console.WriteLine(response.StatusCode);

            await using var input = await response.Content.ReadAsStreamAsync(ct);
            await using var output = new FileStream( resultFilePath, FileMode.Create,FileAccess.Write,FileShare.None, bufferSize: 81920, useAsync: true);

            await input.CopyToAsync(output, bufferSize: 81920, ct);
        }



        public static async Task DownloadContentAsBytesAsync(string url, string resultFilePath, CancellationToken ct = default)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(resultFilePath)!);

            //try
            //{
                using var request = new HttpRequestMessage(HttpMethod.Get, url);

                // You can override/add headers at the request level
                request.Headers.UserAgent.ParseAdd(
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120 Safari/537.36");

                using var response = await _httpClient.GetAsync(url, ct);  // ERROR: здесь программа резко останавливается
                response.EnsureSuccessStatusCode();

                var bytes = await response.Content.ReadAsByteArrayAsync(ct);
                await File.WriteAllBytesAsync(resultFilePath, bytes);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    throw;
            //}
            return;
        }



        public static async Task DownloadContent2Async(string url, string resultFilePath, Encoding? encoding = null, CancellationToken ct = default)
        {
            encoding = encoding ?? Encoding.UTF8;

            Directory.CreateDirectory(Path.GetDirectoryName(resultFilePath)!);

            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            // You can override/add headers at the request level
            request.Headers.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120 Safari/537.36");

            using var response = await _httpClient.GetAsync(url, ct);
            response.EnsureSuccessStatusCode();

            var bytes = await response.Content.ReadAsByteArrayAsync(ct);
            var charset = response.Content.Headers.ContentType?.CharSet;

            //Encoding encoding;

            //try
            //{
            //    encoding = !string.IsNullOrEmpty(charset)
            //        ? Encoding.GetEncoding(charset)
            //        : Encoding.UTF8;
            //}
            //catch
            //{
            //    encoding = Encoding.UTF8;
            //}

            string resultContent = encoding.GetString(bytes);
            await File.WriteAllTextAsync(resultFilePath, resultContent);

            return;
        }




        public static async Task DownloadContentAsync(string url, string resultFilePath, CancellationToken ct = default)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(resultFilePath)!);

            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            // You can override/add headers at the request level
            request.Headers.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120 Safari/537.36");

            using var response = await _httpClient.GetAsync(url, ct);
            response.EnsureSuccessStatusCode();

            var bytes = await response.Content.ReadAsByteArrayAsync(ct);
            var charset = response.Content.Headers.ContentType?.CharSet;

            Encoding encoding;

            try
            {
                encoding = !string.IsNullOrEmpty(charset)
                    ? Encoding.GetEncoding(charset)
                    : Encoding.UTF8;
            }
            catch
            {
                encoding = Encoding.UTF8;
            }

            string resultContent = encoding.GetString(bytes);
            await File.WriteAllTextAsync(resultFilePath, resultContent);

            return;
        }


        static public async Task<string> DownloadUrlContentUtf8Async(string url, string resultFilePath, CancellationToken ct = default)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(resultFilePath)!);

            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            // You can override/add headers at the request level
            request.Headers.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120 Safari/537.36");

            using var response = await _httpClient.GetAsync(url, ct);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync(ct);

            await File.WriteAllTextAsync(resultFilePath, content);
            return await response.Content.ReadAsStringAsync(ct);
        }



        public static async Task<string> GetContentAsync(string url, CancellationToken ct = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            // You can override/add headers at the request level
            request.Headers.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120 Safari/537.36");

            using var response = await _httpClient.GetAsync(url, ct);
            response.EnsureSuccessStatusCode();

            var bytes = await response.Content.ReadAsByteArrayAsync(ct);
            var charset = response.Content.Headers.ContentType?.CharSet;

            Encoding encoding;

            try
            {
                encoding = !string.IsNullOrEmpty(charset)
                    ? Encoding.GetEncoding(charset)
                    : Encoding.UTF8;
            }
            catch
            {
                encoding = Encoding.UTF8;
            }

            return encoding.GetString(bytes);
        }


        static public async Task<string> GetUrlContentUtf8Async(string url, CancellationToken ct = default)
        {
            using var response = await _httpClient.GetAsync(url, ct);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(ct);
        }
    }
}
