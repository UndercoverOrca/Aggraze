using Aggraze.Domain.Types;
using ClosedXML.Excel;

namespace Aggraze.Application.Services;

public interface IExcelGenerationService
{
    XLWorkbook AddInsightsToSheet(string sourceFilePath, IReadOnlyList<IInsightResult> insights);
}