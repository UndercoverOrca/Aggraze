using Aggraze.Domain.Types;

namespace Aggraze.Application.Services;

public interface IFileReaderService
{
    Task<IReadOnlyList<TradeRow>> ReadTradesAsync(string filePath, string sheetName);
    
    Task<IReadOnlyList<TradeRow>> ReadTradesAsync(Stream fileStream, string sheetName);
    
}