using System.Globalization;
using Aggraze.Application;
using Aggraze.Application.Services;
using Aggraze.Domain.Types;
using ClosedXML.Excel;

namespace Aggraze.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IFileStorage fileStorage;
    private readonly IBackgroundJobService backgroundJobService;

    public FileService(IFileStorage fileStorage, IBackgroundJobService backgroundJobService)
    {
        this.fileStorage = fileStorage;
        this.backgroundJobService = backgroundJobService;
    }

    public async Task SaveFile(string fileName, Stream stream, string sheetName, CancellationToken cancellationToken)
    {
        var filePath = await this.fileStorage.SaveAsync(stream, fileName, cancellationToken);
        this.backgroundJobService.EnqueueFileProcessing(filePath, sheetName);
    }

    public async Task<IReadOnlyList<TradeRow>> ReadTradesAsync(string filePath, string? sheetName) =>
        await ReadTradeRowsAsync(() => File.OpenRead(filePath), sheetName);

    public async Task<IReadOnlyList<TradeRow>> ReadTradesAsync(Stream fileStream, string? sheetName) =>
        await ReadTradeRowsAsync(() => fileStream, sheetName);

    private static async Task<IReadOnlyList<TradeRow>> ReadTradeRowsAsync(Func<Stream> streamProvider, string? sheetName)
    {
        var tradeRows = new List<TradeRow>();

        await Task.Run(() =>
        {
            using var stream = streamProvider();
            using var workbook = new XLWorkbook(stream);
            var worksheet = sheetName is not null
                ? workbook.Worksheet(sheetName)
                : workbook.Worksheets.First();

            var rows = worksheet.RangeUsed().RowsUsed().ToList();

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

                var date = ReadDate(data);

                tradeRows.Add(new TradeRow(date, TradeRowDataMapper.Map(data)));
            }
        });

        return tradeRows;
    }

    private static DateOnly ReadDate(Dictionary<string, string> data)
    {
        var dateAsString = data.TryGetValue("Date", out var dateValue)
            ? dateValue
            : string.Empty;

        return DateOnly.FromDateTime(DateTime.ParseExact(
            dateAsString,
            "dd-MM-yyyy HH:mm:ss",
            CultureInfo.InvariantCulture));
    }
}