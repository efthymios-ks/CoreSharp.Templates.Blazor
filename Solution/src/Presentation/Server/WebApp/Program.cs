using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebApp.Extensions;

namespace WebApp;

public static class Program
{
    // Methods
    public static void Main(string[] args)
        => CreateHostBuilder(args)
            .Build()
            .Run();

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureAppLogging();
                webBuilder.UseStartup<Startup>();
            });
}
