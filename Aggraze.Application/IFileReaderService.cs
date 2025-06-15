using Aggraze.Domain;

namespace Aggraze.Application;

public interface IFileReaderService
{ 
    Task<List<TradeData>> ReadTradesAsync(string filePath);
}