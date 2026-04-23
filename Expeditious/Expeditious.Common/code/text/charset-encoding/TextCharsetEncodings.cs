
using System.Text;


namespace Expeditious.Common
{
    static public class TextCharsetEncodings
    {
        private static readonly object SyncRoot = new();
        private static bool _alreadyRegistered;

        private static void RegisterEncodingProviderInstance()
        {
            if (_alreadyRegistered)
                return;

            lock (SyncRoot)
            {
                if (_alreadyRegistered)
                    return;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                _alreadyRegistered = true;
            }
        }


        public static Encoding Utf8 => Encoding.UTF8;
        public static Encoding Ascii => Encoding.ASCII;
        public static Encoding Utf32 => Encoding.UTF32;


        public static Encoding CyrillicWindows1251
        {
            get
            {
                RegisterEncodingProviderInstance();
                return Encoding.GetEncoding(1251);
            }
        }

        public static Encoding CyrillicDosCp866
        {
            get
            {
                RegisterEncodingProviderInstance();
                return Encoding.GetEncoding(866);
            }
        }

        public static Encoding TurkishWindows1254
        {
            get
            {
                RegisterEncodingProviderInstance();
                return Encoding.GetEncoding(1254);
            }
        }

        public static Encoding GetByEnum(TextCharsetEncodingKind encodingKind) =>
                encodingKind switch
                {
                    TextCharsetEncodingKind.Utf8 => Utf8,
                    TextCharsetEncodingKind.Ascii => Ascii,
                    TextCharsetEncodingKind.Utf32 => Utf32,
                    TextCharsetEncodingKind.CyrillicWindows1251 => CyrillicWindows1251,
                    TextCharsetEncodingKind.CyrillicDosCp866 => CyrillicDosCp866,
                    TextCharsetEncodingKind.TurkishWindows1254 => TurkishWindows1254,
                    _ => throw new ArgumentOutOfRangeException(nameof(encodingKind), encodingKind, "Unsupported text encoding.")
                };
    }
}
