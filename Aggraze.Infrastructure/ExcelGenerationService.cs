using Aggraze.Application;
using Aggraze.Domain;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Aggraze.Infrastructure;

public class ExcelGenerationService : IExcelGenerationService
{
    public Workbook AddInsightSheet(string sourceFilePath, IReadOnlyList<InsightResult> insights, string outputFilePath)
    {
        // using (var workbook = new XLWorkbook(sourceFilePath))
        // {
        //     var insightsSheet = workbook.Worksheets.Add("Insights");
        // }
        throw new NotImplementedException();
    }
}