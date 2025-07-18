using Aggraze.Application.Insights;
using Aggraze.Application.Services;
using Aggraze.Domain.Calculators;
using Aggraze.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aggraze;

public class Startup
{
    public ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        
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

        return services.BuildServiceProvider();
    }
}