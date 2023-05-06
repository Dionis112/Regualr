using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Regular.Console.Domain;
using Regular.Console.Domain.Interfaces;
using Regular.Console.Handlers;
using Regular.Console.Infrastructure.Repositories;

namespace Regular.Console.Application
{
    public class Configurations
    {
        private List<Configuration> ConfigurationsList { get; set; }

        public void Create(string path)
        {
            ISerializer<List<Configuration>> serializer = null;

            var fileExtension = GetFilenameExtension(path);
            switch (fileExtension)
            {
                case FilenameExtension.CSV:
                    serializer = new CsvSerializer<List<Configuration>>();
                    ConfigurationsList = ParceFile(serializer, path);
                    break;
                case FilenameExtension.XML:
                    serializer = new XmlSerializer<List<Configuration>>();
                    ConfigurationsList = ParceFile(serializer, path);
                    break;
                default:
                    throw new Exception("Unknown filename extension");
            }

            Save();
        }

        private void Save()
        {
            if (ConfigurationsList.Count() > 0)
            {
                using (IRepository<Configuration> context = new ConfigurationRepository())
                {
                    foreach (var configuration in ConfigurationsList)
                    {
                        context.Create(configuration);
                    }

                    context.Save();
                }
            }
        }


        private List<Configuration> ParceFile(ISerializer<List<Configuration>> serializer, string path)
        {
            return serializer.Deserialize(path);
        }

        private FilenameExtension GetFilenameExtension(string path)
        {
            return (FilenameExtension)Enum.Parse(typeof(FilenameExtension), Path.GetExtension(path)?.Remove(0, 1).ToUpper());
        }
    }
}
