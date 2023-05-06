using System.Xml;
using Regular.Console.Domain.Interfaces;

namespace Regular.Console.Handlers
{
    public class XmlSerializer<T> : ISerializer<T> where T : class
    {
        public T Deserialize(string path)
        {

            var xmlString = File.ReadAllText(path);
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (var stringReader = new StringReader(xmlString))
            {
                var xmlReader = new XmlTextReader(stringReader);
                return serializer.Deserialize(xmlReader) as T;
            }
        }

        public string Serialize(T item)
        {
            XmlDocument xmlDocument = new XmlDocument();

            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (var xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, item);
                xmlStream.Position = 0;

                xmlDocument.Load(xmlStream);
            }
            return xmlDocument.InnerXml;
        }
    }
}
