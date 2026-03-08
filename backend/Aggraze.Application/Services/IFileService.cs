using Aggraze.Domain.Types;

namespace Aggraze.Application.Services;

public interface IFileService
{
    Task SaveFile(string fileName, Stream stream, string sheetName, CancellationToken cancellationToken);

    Task<IReadOnlyList<TradeRow>> ReadTradesAsync(string filePath, string sheetName);

    Task<IReadOnlyList<TradeRow>> ReadTradesAsync(Stream fileStream, string sheetName);
}