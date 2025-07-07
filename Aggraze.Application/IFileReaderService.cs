using Aggraze.Domain;

namespace Aggraze.Application;

public interface IFileReaderService
{ 
    Task<IReadOnlyList<TradeRow>> ReadTradesAsync(string filePath, string sheetName);
}