using Aggraze.Domain;
using ClosedXML.Excel;

namespace Aggraze.Application;

public interface IExcelGenerationService
{
    XLWorkbook AddInsightSheet(string sourceFilePath, IReadOnlyList<InsightResult> insights);
}