using Aggraze.Application;
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

            try
            {
                // Resolve services
                var fileReader = serviceProvider.GetRequiredService<IFileReaderService>();
                var orchestrator = serviceProvider.GetRequiredService<AggregationOrchestratorService>();
                var excelGenerator = serviceProvider.GetRequiredService<IExcelGenerationService>();
                var fileWriter = serviceProvider.GetRequiredService<IFileWriterService>();

                // Read trades
                var trades = await fileReader.ReadTradesAsync(filePath);

                // Run insights
                var insights = orchestrator.RunAllInsights(trades);

                // Generate Excel
                var workbook = excelGenerator.GenerateWorkbook(insights);

                // Ask for output path
                Console.WriteLine("Enter path to save the results Excel file:");
                var outputPath = Console.ReadLine();

                fileWriter.SaveWorkbook(workbook, outputPath);

                Console.WriteLine($"Insights successfully saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }
    }
}
