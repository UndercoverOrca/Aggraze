using Aggraze.Domain;
using ClosedXML.Excel;

namespace Aggraze.Application;

public interface IExcelGenerationService
{
    XLWorkbook AddInsightsToSheet(string sourceFilePath, IReadOnlyList<InsightResult> insights);
}