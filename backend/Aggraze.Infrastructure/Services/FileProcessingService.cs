using Aggraze.Application;
using Aggraze.Application.Services;

namespace Aggraze.Infrastructure.Services;

public class FileProcessingService : IFileProcessingService
{
    private readonly IFileStorage fileStorage;
    private readonly IFileService fileReaderService;

    public FileProcessingService(IFileStorage fileStorage, IFileService fileReaderService)
    {
        this.fileStorage = fileStorage;
        this.fileReaderService = fileReaderService;
    }

    public async Task ProcessAsync(string filePath, string sheetName)
    {
        await using var stream = await fileStorage.OpenReadAsync(filePath, CancellationToken.None);
        var data = await this.fileReaderService.ReadTradesAsync(stream, sheetName);
    }
}