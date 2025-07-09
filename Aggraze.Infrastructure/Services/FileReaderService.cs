using System.Globalization;
using Aggraze.Application;
using Aggraze.Domain;
using ClosedXML.Excel;

namespace Aggraze.Infrastructure;

public class FileReaderService : IFileReaderService
{
    public async Task<IReadOnlyList<TradeRow>> ReadTradesAsync(string filePath, string? sheetName)
    {
        var tradeRows = new List<TradeRow>();

        await Task.Run(() =>
        {
            using var workbook = new XLWorkbook(filePath);
            var worksheet = sheetName is not null
                ? workbook.Worksheet(sheetName)
                : workbook.Worksheets.FirstOrDefault();

            var rows = worksheet!.RangeUsed()!.RowsUsed().ToList();

            if (rows.Count < 2)
            {
                throw new InvalidOperationException("The sheet must contain at least one header row and one data row");
            }

            var headerRow = rows[0];

            var headers = headerRow
                .Cells()
                .Select(cell => cell.GetValue<string>())
                .ToList();

            foreach (var row in rows.Skip(1))
            {
                var cellValues = row.Cells().ToList();
                var data = new Dictionary<string, string>();

                for (var i = 0; i < headers.Count && i < cellValues.Count; i++)
                {
                    var header = headers[i];
                    var value = cellValues[i].GetValue<string>().Trim();
                    data[header] = value;
                }

                var dateAsString = data.TryGetValue("Date", out var dateValue)
                    ? dateValue
                    : string.Empty;

                var date = DateOnly.FromDateTime(DateTime.ParseExact(
                    dateAsString,
                    "dd/MM/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture));
                
                tradeRows.Add(new TradeRow(date, TradeRowDataMapper.Map(data)));
            }
        });

        return tradeRows;
    }
}