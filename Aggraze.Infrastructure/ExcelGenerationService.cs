using System.Globalization;
using Aggraze.Application;
using Aggraze.Domain;
using ClosedXML.Excel;

namespace Aggraze.Infrastructure;

public class ExcelGenerationService : IExcelGenerationService
{
    public XLWorkbook AddInsightSheet(string sourceFilePath, IReadOnlyList<InsightResult> insights)
    {
        var workbook = new XLWorkbook(sourceFilePath);
        var insightSheet = workbook.Worksheets.Add("Insights");
        
        var rowIndex = 1;
        var columnIndex = 1;
        const int headerSize = 12;
        foreach (var insight in insights)
        {
            // Set header
            insightSheet.Range(rowIndex, columnIndex, rowIndex, columnIndex + 12).Merge();
            insightSheet.Cell(rowIndex, columnIndex).Style.Font.SetBold();
            insightSheet.Cell(rowIndex, columnIndex).Style.Font.FontSize = headerSize;
            insightSheet.Cell(rowIndex, columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            insightSheet.Cell(rowIndex, columnIndex).Value = insight.InsightName;
            
            rowIndex++;

            for (var i = 1; i < columnIndex + 12; i++)
            {
                insightSheet.Cell(rowIndex, columnIndex + i).Value =
                    CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(i + 1);
                
                insightSheet.Cell(rowIndex, columnIndex + i).Style.Font.SetBold();
            }
            
            rowIndex++;
            
            foreach (var yearMonthData in insight.YearMonthData)
            {
                var columnIndexForData = columnIndex;

                insightSheet.Cell(rowIndex, columnIndexForData).Value = yearMonthData.Key;
                insightSheet.Cell(rowIndex, columnIndexForData).Style.Font.SetBold();

                columnIndexForData++;

                foreach (var monthlyData in yearMonthData.Value)
                {
                    insightSheet.Cell(rowIndex, columnIndexForData).Value = monthlyData.Value;
                    columnIndexForData++;
                }
            }
            
            rowIndex++;

            foreach (var summary in insight.Summary)
            {
                var columnIndexForData = columnIndex;

                if (columnIndexForData == columnIndex)
                {
                    insightSheet.Cell(rowIndex, columnIndexForData).Value = summary.Key;
                    insightSheet.Cell(rowIndex, columnIndexForData).Style.Font.SetBold();
                }
                else
                {
                    insightSheet.Cell(rowIndex, columnIndexForData).Value = summary.Value;
                }

                columnIndexForData++;
            }
        }
        
        return workbook;
    }
}