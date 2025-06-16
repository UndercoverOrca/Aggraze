using Aggraze.Domain;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Aggraze.Application;

public interface IExcelGenerationService
{
    Workbook AddInsightSheet(string sourceFilePath, IReadOnlyList<InsightResult> insights, string outputFilePath);
}