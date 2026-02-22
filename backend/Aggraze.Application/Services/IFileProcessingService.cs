namespace Aggraze.Application.Services;

public interface IFileProcessingService
{
    Task ProcessAsync(string filePath, string sheetName);
}