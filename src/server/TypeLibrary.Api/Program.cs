using Mimirorg.Common.Abstract;
using Mimirorg.Common.Models;

namespace TypeLibrary.Api;

public class Program
{
    /// <summary>
    /// Main application method
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Create the host builder
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole().AddConsoleFormatter<CustomTimePrefixingFormatter, CustomWrappingConsoleFormatterOptions>();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                    .ConfigureAppConfiguration(configurationBuilder =>
                    {
                        configurationBuilder.AddJsonFile($"{Directory.GetCurrentDirectory()}/appsettings.local.json", true);
                    });
            });

}