
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace Expeditious.Common
{
    public static class SimpleXmlHelper
    {
        public static void SerializeToXml<T>(T obj, string xmlFilePath)
        {
            ArgumentNullException.ThrowIfNull(obj);

            if (string.IsNullOrWhiteSpace(xmlFilePath))
                throw new ArgumentException("XML file path is null or empty.", nameof(xmlFilePath));

            var serializer = new XmlSerializer(typeof(T));

            // Убираем xmlns:xsi и xmlns:xsd
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true,
                IndentChars = "    ",
                NewLineChars = Environment.NewLine,
                NewLineHandling = NewLineHandling.Replace
            };

            using var stream = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
            using var writer = XmlWriter.Create(stream, settings);

            serializer.Serialize(writer, obj, namespaces);
        }


        public static T DeserializeFromXml<T>(string xmlFilePath)
        {
            if (string.IsNullOrWhiteSpace(xmlFilePath))
                throw new ArgumentException("XML file path is null or empty.", nameof(xmlFilePath));

            if (!File.Exists(xmlFilePath))
                throw new FileNotFoundException("XML file not found.", xmlFilePath);

            var serializer = new XmlSerializer(typeof(T));

            using var stream = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = XmlReader.Create(stream);

            var result = serializer.Deserialize(reader);

            if (result is not T typedResult)
                throw new InvalidOperationException(
                    $"Failed to deserialize XML file '{xmlFilePath}' to type '{typeof(T).FullName}'.");

            return typedResult;
        }
    }
}
