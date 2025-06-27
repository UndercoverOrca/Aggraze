using System.Globalization;
using Aggraze.Application;
using Aggraze.Domain;
using ClosedXML.Excel;

namespace Aggraze.Infrastructure;

public class ExcelGenerationService : IExcelGenerationService
{
    private static CultureInfo culture = CultureInfo.InvariantCulture;
    private const int FirstColumnMargin = 1;

    public XLWorkbook AddInsightSheet(string sourceFilePath, IReadOnlyList<InsightResult> insights)
    {
        var workbook = new XLWorkbook(sourceFilePath);
        var insightSheet = workbook.Worksheets.Add("Insights");

        var rowIndex = 1;
        const int columnIndex = 1;
        const int headerSize = 12;

        foreach (var insight in insights)
        {
            SetHeader(insightSheet, rowIndex, columnIndex, headerSize, insight);

            rowIndex++;

            SetMonths(columnIndex, insightSheet, rowIndex);

            rowIndex++;

            SetData(insight, columnIndex, insightSheet, rowIndex);

            rowIndex++;

            SetSummary(insight, columnIndex, insightSheet, rowIndex);

            rowIndex++;
            rowIndex++;
            rowIndex++;
        }

        return workbook;
    }

    private static void SetSummary(InsightResult insight, int columnIndex, IXLWorksheet insightSheet, int rowIndex)
    {
        var columnIndexForData = columnIndex;

        foreach (var summary in insight.Summary.data)
        {
            if (columnIndexForData == columnIndex)
            {
                insightSheet.Cell(rowIndex, columnIndexForData).Value = insight.Summary.SummaryType.ToString();
                insightSheet.Cell(rowIndex, columnIndexForData).Style.Font.SetBold();
                insightSheet.Cell(rowIndex, columnIndexForData).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            }
            else
            {
                insightSheet.Cell(rowIndex, columnIndexForData).Value = summary.Value;
            }

            columnIndexForData++;
        }
    }

    private static void SetData(InsightResult insight, int columnIndex, IXLWorksheet insightSheet, int rowIndex)
    {
        foreach (var year in insight.YearMonthData)
        {
            var columnIndexForData = columnIndex;

            insightSheet.Cell(rowIndex, columnIndexForData).Value = year.Key;
            insightSheet.Cell(rowIndex, columnIndexForData).Style.Font.SetBold();

            columnIndexForData++;

            // TODO: When a month is empty, we should skip it.
            foreach (var monthlyData in year.Value)
            {
                var monthIndex = (DateTime.ParseExact(monthlyData.Key, "MMMM", culture).Month) + FirstColumnMargin;
                
                insightSheet.Cell(rowIndex, monthIndex).Value = monthlyData.Value;
                columnIndexForData++;
            }
        }
    }

    private static void SetMonths(int columnIndex, IXLWorksheet insightSheet, int rowIndex)
    {
        var columnIndexForMonths = columnIndex + FirstColumnMargin;
        for (var i = 0; i < columnIndex + 12; i++)
        {
            insightSheet.Cell(rowIndex, columnIndexForMonths + i).Value =
                culture.DateTimeFormat.GetMonthName(i + FirstColumnMargin);

            insightSheet.Cell(rowIndex, columnIndexForMonths + i).Style.Font.SetBold();
        }
    }

    private static void SetHeader(IXLWorksheet insightSheet, int rowIndex, int columnIndex, int headerSize, InsightResult insight)
    {
        insightSheet.Range(rowIndex, columnIndex, rowIndex, columnIndex + 12).Merge();
        insightSheet.Cell(rowIndex, columnIndex).Style.Font.SetBold();
        insightSheet.Cell(rowIndex, columnIndex).Style.Font.FontSize = headerSize;
        insightSheet.Cell(rowIndex, columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        insightSheet.Cell(rowIndex, columnIndex).Value = insight.InsightName;
    }
}