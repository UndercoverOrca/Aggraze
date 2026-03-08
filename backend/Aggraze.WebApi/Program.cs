using Serilog;

namespace Aggraze.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            CreateHostBuilder(args).Build().Run();
            Log.Logger.Information("Web API starting");
        }
        catch (Exception e)
        {
            Log.Logger.Fatal(e, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(wb =>
            {
                wb.ConfigureAppConfiguration((context, builder) =>
                    {
                    })
                    .UseStartup<Startup>();
            });

}