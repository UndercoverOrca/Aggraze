using Aggraze.Application;
using Aggraze.Application.Insights;
using Aggraze.Domain.Calculators;
using Aggraze.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Aggraze;

public class Startup
{
    public ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        
        // Register Domain services
        services.AddScoped<IAverageRunningTimeCalculator, AverageRunningTimeCalculator>();
        services.AddScoped<IMaximumDrawdownCalculator, MaximumDrawdownCalculator>();

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
        services.AddScoped<IInsight, MaximumRRAllWinningTrades>();
        services.AddScoped<IInsight, Mutation>();

        // Optional: Logging

        return services.BuildServiceProvider();
    }
}