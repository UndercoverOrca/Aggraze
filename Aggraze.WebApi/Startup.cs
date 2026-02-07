using Aggraze.Application.Insights;
using Aggraze.Application.Services;
using Aggraze.Domain.Calculators;
using Aggraze.Infrastructure.Services;

namespace Aggraze.WebApi;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddSwaggerGen();
        services.AddControllers();
        services.AddHealthChecks();

        services.AddEndpointsApiExplorer();

        // Register Domain services
        services.AddScoped<IAverageRunningTimeCalculator, AverageRunningTimeCalculator>();
        services.AddScoped<IMutationCalculator, MutationCalculator>();
        services.AddScoped<IMaximumDrawdownCalculator, MaximumDrawdownCalculator>();
        services.AddScoped<IMaximumRiskRewardWinningTradesCalculator, MaximumRiskRewardWinningTradesCalculator>();

        // Register application services
        services.AddScoped<AggregationOrchestratorService>();

        // Register infrastructure services
        services.AddScoped<IFileReaderService, FileReaderService>();
        services.AddScoped<IFileWriterService, FileWriterService>();
        services.AddScoped<IExcelGenerationService, ExcelGenerationService>();

        // Register insights
        services.AddScoped<IInsight, AverageRunningTime>();
        services.AddScoped<IInsight, AverageRunningTimeLosers>();
        services.AddScoped<IInsight, AverageRunningTimeWinners>();
        services.AddScoped<IInsight, MaximumDrawdown>();
        services.AddScoped<IInsight, MaximumRiskRewardWinningTrades>();
        services.AddScoped<IInsight, Mutation>();

        // Optional: Logging
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseHealthChecks("/health");
        app.UseEndpoints(endpoint =>
        {
            endpoint.MapControllers();
            endpoint.MapHealthChecks("/health");
        });

        app.UseHttpsRedirection();

        // TODO: add check for environment
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Aggraze web API");
            options.RoutePrefix = string.Empty;
        });
    }
}