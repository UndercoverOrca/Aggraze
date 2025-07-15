using System.Globalization;
using Aggraze.Application.Services;
using Aggraze.Domain.Types;
using ClosedXML.Excel;

namespace Aggraze.Infrastructure.Services;

public class ExcelGenerationService : IExcelGenerationService
{
    private static CultureInfo culture = CultureInfo.InvariantCulture;
    private const int FirstColumnMargin = 1;
    private static int rowIndex = 1;

    public XLWorkbook AddInsightsToSheet(string sourceFilePath, IReadOnlyList<IInsightResult> insights)
    {
        var workbook = new XLWorkbook(sourceFilePath);
        var insightSheet = workbook.Worksheets.Add("Insights");

        const int columnIndex = 1;

        foreach (var insight in insights)
        {
            SetHeader(insightSheet, columnIndex, insight);

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

    private static void SetHeader(IXLWorksheet insightSheet, int columnIndex, IInsightResult insight)
    {
        insightSheet.Range(rowIndex, columnIndex + FirstColumnMargin, rowIndex, columnIndex + 12).Merge();
        insightSheet.Cell(rowIndex, columnIndex + FirstColumnMargin).Style.Font.SetBold();
        insightSheet.Cell(rowIndex, columnIndex + FirstColumnMargin).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        insightSheet.Cell(rowIndex, columnIndex + FirstColumnMargin).Value = insight.InsightName;
        insightSheet.Cell(rowIndex, columnIndex + FirstColumnMargin).Style.Fill.BackgroundColor = XLColor.LightGray;
    }

    private static void SetMonths(int columnIndex, IXLWorksheet insightSheet)
    {
        var columnIndexForMonths = columnIndex + FirstColumnMargin;
        for (var i = 0; i < columnIndex + 12; i++)
        {
            insightSheet.Cell(rowIndex, columnIndexForMonths + i).Value = 
                culture.DateTimeFormat.GetAbbreviatedMonthName(i + FirstColumnMargin);
        }
        
        insightSheet.Range(rowIndex, columnIndex + FirstColumnMargin, rowIndex, columnIndex + 12).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        insightSheet.Range(rowIndex, columnIndex, rowIndex, columnIndex + 12).Style.Font.SetBold();
        insightSheet.Range(rowIndex, columnIndex, rowIndex, columnIndex + 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    }

    private static void SetData(IInsightResult insight, int columnIndex, IXLWorksheet insightSheet)
    {
        foreach (var year in insight.YearMonthData)
        {
            insightSheet.Cell(rowIndex, columnIndex).Value = year.Key;
            insightSheet.Cell(rowIndex, columnIndex).Style.Font.SetBold();
            insightSheet.Cell(rowIndex, columnIndex).Style.Border.RightBorder = XLBorderStyleValues.Thin;

            foreach (var monthlyData in year.Value)
            {
                var monthIndex = (DateTime.ParseExact(monthlyData.Key, "MMMM", culture).Month) + FirstColumnMargin;
                insightSheet.Cell(rowIndex, monthIndex).Value = monthlyData.Value.ToFormattedString();
                insightSheet.Cell(rowIndex, monthIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            
            rowIndex++;
        }
        
        insightSheet.Range(rowIndex, columnIndex, rowIndex, columnIndex + 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    }

    private static void SetSummary(IInsightResult insight, int columnIndex, IXLWorksheet insightSheet)
    {
        insightSheet.Cell(rowIndex, columnIndex).Value = insight.Summary.SummaryType.ToString();
        insightSheet.Cell(rowIndex, columnIndex).Style.Font.SetBold();
        insightSheet.Cell(rowIndex, columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        insightSheet.Cell(rowIndex, columnIndex).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        insightSheet.Range(rowIndex, columnIndex, rowIndex, columnIndex + 12).Style.Font.SetBold();

        foreach (var summary in insight.Summary.Data)
        {
            var monthIndex = (DateTime.ParseExact(summary.Key, "MMMM", culture).Month) + FirstColumnMargin;
            insightSheet.Cell(rowIndex, monthIndex).Value = summary.Value.ToFormattedString();
        }
    }
}