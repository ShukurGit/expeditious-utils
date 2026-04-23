

namespace Expeditious.Common
{
    using CsvHelper;
    using System.Globalization;
    using System.Text;


    public class CsvDataProcessor
    {
        public static List<T> ReadCsv<T>(String csvPath, String delimiter = ",", Boolean hasHeader = true, Encoding? enc = null) where T : class
        {
            if (!File.Exists(csvPath))
            {
                throw new FileNotFoundException($"ERROR: File `{csvPath}` not found.");
            }

            enc ??= new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = delimiter,
                HasHeaderRecord = hasHeader,
                BadDataFound = ctx => Console.Error.WriteLine($"Bad data at row   {ctx.RawRecord}"),
                MissingFieldFound = null,
                TrimOptions = CsvHelper.Configuration.TrimOptions.Trim
            };

            using var reader = new StreamReader(csvPath, enc);
            using var csv = new CsvReader(reader, config);
            return csv.GetRecords<T>().ToList();
        }


        static public String WriteCsv<T>(List<T> listObjects, String? outCsvPath, String delimiter = ",", Boolean hasHeader = true, Encoding? enc = null) where T : class
        {
            outCsvPath = String.IsNullOrWhiteSpace(outCsvPath) ? System.IO.Path.GetTempFileName().Replace(".tmp", ".csv") : outCsvPath;

            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = delimiter,              // если нужен ; вместо ,
                Encoding = enc == null ? Encoding.UTF8 : enc,     // можно явно указать кодировку
                HasHeaderRecord = hasHeader
            };

            using (var writer = new StreamWriter(outCsvPath, false, config.Encoding))
            {
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(listObjects);
                }
            }

            return outCsvPath;
        }



        public static List<Dictionary<string, string?>>? ReadCsvToDynamic(String csvPath, String delimiter = ",", Boolean hasHeader = true, Encoding? enc = null)
        {
            if (!File.Exists(csvPath))
            {
                throw new FileNotFoundException($"ERROR: File `{csvPath}` not found.");
            }

            enc ??= new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = delimiter,
                HasHeaderRecord = hasHeader,
                BadDataFound = ctx => Console.Error.WriteLine($"Bad data at row   {ctx.RawRecord}"),
                MissingFieldFound = null,
                TrimOptions = CsvHelper.Configuration.TrimOptions.Trim
            };

            using var reader = new StreamReader(csvPath, enc);
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<dynamic>().Select(r => (IDictionary<string, object>)r)
                .Select(dict => dict.ToDictionary(k => k.Key, v => v.Value?.ToString())).ToList();
            return records;
        }
    }
}