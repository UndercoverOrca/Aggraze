using System.Globalization;
using Aggraze.Application.Services;
using Aggraze.Domain.Types;
using ClosedXML.Excel;

namespace Aggraze.Infrastructure.Services;

public class FileReaderService : IFileReaderService
{
    public async Task<IReadOnlyList<TradeRow>> ReadTradesAsync(string filePath, string? sheetName) =>
        await ReadTradeRowsAsync2(() => File.OpenRead(filePath), sheetName);

    public async Task<IReadOnlyList<TradeRow>> ReadTradesAsync(Stream fileStream, string? sheetName) => 
        await ReadTradeRowsAsync2(() => fileStream, sheetName);

    private static async Task<IReadOnlyList<TradeRow>> ReadTradeRowsAsync2(Func<Stream> streamProvider, string sheetName)
    {
        var tradeRows = new List<TradeRow>();

        await Task.Run(() =>
        {
            using var stream = streamProvider();
            using var workbook = new XLWorkbook(stream);
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