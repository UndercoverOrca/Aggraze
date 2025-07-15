using Aggraze.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aggraze
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // Set up DI
            var startup = new Startup();
            var serviceProvider = startup.ConfigureServices();

            // Ask user for file path
            Console.WriteLine("Enter full path to the trade data Excel file:");
            var filePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Invalid file path. Exiting.");
                return;
            }

            Console.WriteLine("Enter the name of the data sheet:");
            var sheetName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(sheetName))
            {
                Console.WriteLine("Invalid sheet name. Exiting.");
                return;
            }
            
            try
            {
                // Resolve services
                var fileReader = serviceProvider.GetRequiredService<IFileReaderService>();
                var orchestrator = serviceProvider.GetRequiredService<AggregationOrchestratorService>();
                var excelGenerator = serviceProvider.GetRequiredService<IExcelGenerationService>();
                var fileWriter = serviceProvider.GetRequiredService<IFileWriterService>();

                // Read trades
                var trades = await fileReader.ReadTradesAsync(filePath, sheetName);

                // Run insights
                var insights = orchestrator.RunAllInsights(trades);
                
                var outputPath = filePath.Replace(".xlsx", "_insights.xlsx");
                
                // Generate Excel
                var workbook = excelGenerator.AddInsightsToSheet(filePath, insights);

                fileWriter.SaveWorkbook(workbook, outputPath);

                Console.WriteLine($"Insights successfully saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
