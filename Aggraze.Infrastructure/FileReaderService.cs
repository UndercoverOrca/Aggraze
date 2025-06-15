using Aggraze.Application;
using Aggraze.Domain;

namespace Aggraze.Infrastructure;

public class FileReaderService : IFileReaderService
{
    public async Task<List<TradeData>> ReadTradesAsync(string filePath)
    {
        throw new NotImplementedException();
    }
}