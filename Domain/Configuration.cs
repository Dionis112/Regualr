using System.Xml;
using System.Xml.Serialization;

namespace Regular.Console.Domain
{
    public class Configuration
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
