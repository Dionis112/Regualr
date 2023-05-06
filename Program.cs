using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Regular.Console.Application;
using System.Reflection;
using System.Text;

//class Program
//{
//    static async Task<int> Main(string[] args)
//    {
//        var host = CreateHostBuilder();
//        await host.RunConsoleAsync();
//        return Environment.ExitCode;
//    }

//    private static IHostBuilder CreateHostBuilder()
//    {
//        return Host.CreateDefaultBuilder()
//        .ConfigureServices(services =>
//        {
//            services.AddHostedService<Worker>();
//        });
//    }
//}

//public class Worker : IHostedService
//{
//    public Task StartAsync(CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }

//    public Task StopAsync(CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }
//}

//Configurations configurations = new Configurations
//{
//    ListConfigurations = new List<Configuration>()
//    {
//        new Configuration
//        {
//            Description = "A",
//            Name = "A",
//        }
//    }
//};

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
var xml = Path.Combine(path!, "Configuration.xml");
new Configurations().Create(xml);
//var csv = Path.Combine(path!, "Configuration.csv");

////var value = new CsvSerializer<List<Configuration>>().Deserialize(csv);
////var xml = Path.Combine(path!, "Configuration.xml");
////var t = new XmlSerializer<List<Configuration>>().Deserialize(xml);
////Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
////var csv = Path.Combine(path!, "Configuration.csv");

//Console.ReadLine();