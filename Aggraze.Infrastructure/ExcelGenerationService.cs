using System.Globalization;
using Aggraze.Application;
using Aggraze.Domain;
using ClosedXML.Excel;

namespace Aggraze.Infrastructure;

public class ExcelGenerationService : IExcelGenerationService
{
    private static CultureInfo culture = CultureInfo.InvariantCulture;
    private const int FirstColumnMargin = 1;
    private static int rowIndex = 1;

    public XLWorkbook AddInsightsToSheet(string sourceFilePath, IReadOnlyList<InsightResult> insights)
    {
        var workbook = new XLWorkbook(sourceFilePath);
        var insightSheet = workbook.Worksheets.Add("Insights");

        const int columnIndex = 1;
        const int headerSize = 12;

        foreach (var insight in insights)
        {
            SetHeader(insightSheet, columnIndex, headerSize, insight);

            rowIndex++;

            SetMonths(columnIndex, insightSheet);

            rowIndex++;

            SetData(insight, columnIndex, insightSheet);

            SetSummary(insight, columnIndex, insightSheet);

            rowIndex++;
            rowIndex++;
            rowIndex++;
        }

        return workbook;
    }

    private static void SetHeader(IXLWorksheet insightSheet, int columnIndex, int headerSize, InsightResult insight)
    {
        insightSheet.Range(rowIndex, columnIndex, rowIndex, columnIndex + 12).Merge();
        insightSheet.Cell(rowIndex, columnIndex).Style.Font.SetBold();
        insightSheet.Cell(rowIndex, columnIndex).Style.Font.FontSize = headerSize;
        insightSheet.Cell(rowIndex, columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        insightSheet.Cell(rowIndex, columnIndex).Value = insight.InsightName;
        insightSheet.Cell(rowIndex, columnIndex).Style.Fill.BackgroundColor = XLColor.LightGray;
    }

    private static void SetMonths(int columnIndex, IXLWorksheet insightSheet)
    {
        var columnIndexForMonths = columnIndex + FirstColumnMargin;
        for (var i = 0; i < columnIndex + 12; i++)
        {
            insightSheet.Cell(rowIndex, columnIndexForMonths + i).Value =
                culture.DateTimeFormat.GetMonthName(i + FirstColumnMargin);

            insightSheet.Cell(rowIndex, columnIndexForMonths + i).Style.Font.SetBold();
        }
    }

    private static void SetData(InsightResult insight, int columnIndex, IXLWorksheet insightSheet)
    {
        foreach (var year in insight.YearMonthData)
        {
            insightSheet.Cell(rowIndex, columnIndex).Value = year.Key;
            insightSheet.Cell(rowIndex, columnIndex).Style.Font.SetBold();

            foreach (var monthlyData in year.Value)
            {
                var monthIndex = (DateTime.ParseExact(monthlyData.Key, "MMMM", culture).Month) + FirstColumnMargin;
                insightSheet.Cell(rowIndex, monthIndex).Value = monthlyData.Value.ToString("g");
            }
            
            rowIndex++;
        }
    }

    private static void SetSummary(InsightResult insight, int columnIndex, IXLWorksheet insightSheet)
    {
        insightSheet.Cell(rowIndex, columnIndex).Value = insight.Summary.SummaryType.ToString();
        insightSheet.Cell(rowIndex, columnIndex).Style.Font.SetBold();
        insightSheet.Cell(rowIndex, columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

        foreach (var summary in insight.Summary.data)
        {
            var monthIndex = (DateTime.ParseExact(summary.Key, "MMMM", culture).Month) + FirstColumnMargin;
            insightSheet.Cell(rowIndex, monthIndex).Value = summary.Value.ToString("g");
            insightSheet.Range(rowIndex, columnIndex, rowIndex, columnIndex + 12).Style.Font.SetBold();
        }
    }
}