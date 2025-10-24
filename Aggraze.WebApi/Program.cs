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

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddHealthChecks();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapHealthChecks("/healthy");

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
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