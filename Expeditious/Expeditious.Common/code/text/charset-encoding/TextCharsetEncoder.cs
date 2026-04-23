
using System.Text;


namespace Expeditious.Common
{
    public static class TextCharsetEncoder
    {
        static public (bool isSuccess, string content, string msg) ReadFileContent(String filePath, Encoding encoding)
        {
            try
            {
                string content = File.ReadAllText(filePath, encoding);
                return (isSuccess: true, content, msg: $"Successfully read file ″{filePath}″ in encoding {encoding}.");
            }
            catch (Exception ex)
            {
                return (isSuccess: false, content: string.Empty,
                    msg: $"Error occurs during re read file ″{filePath}″ in this encoding {encoding}. \r\n {ex.Message}");
            }

        }



        static public (bool isSuccess, string msg) WriteToUnicodeFile(string iniFilePath, Encoding iniEncoding, string destFilePath)
        {
            var readResult = ReadFileContent(iniFilePath, iniEncoding);
            if (!readResult.isSuccess)
                return (isSuccess: readResult.isSuccess, msg: readResult.msg);

            try
            {
                File.WriteAllText(destFilePath, readResult.content);
                return (true, "OK");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }



        static public (Boolean success, String msg) ConvertTextFileCharset(
            Encoding iniEncoding, string iniFilePath, Encoding outEncoding, string destFilePath)
        {
            var iniFile = ReadFileContent(iniFilePath, iniEncoding);
            if (!iniFile.isSuccess) return (isSuccess: false, msg: iniFile.msg);

            //Byte[] bytesUtf = Encoding.UTF8.GetBytes(content);
            //Byte[] bytesCyr = Encoding.Convert(Encoding.UTF8, TextEncodings.CyrillicWindows1251, bytesUtf);
            //File.WriteAllBytes(destFilePath, bytesCyr);

            try
            {
                File.WriteAllText(destFilePath, iniFile.content, outEncoding);
                return (true, "OK");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }



        static public (bool isSuccess, string msg) WriteCyrillicWindows1251ToUnicodeFile(string iniFilePath, Encoding iniEncoding, string destFilePath)
        {
            return WriteToUnicodeFile(iniFilePath, TextCharsetEncodings.CyrillicWindows1251, destFilePath);
        }
    }
}
