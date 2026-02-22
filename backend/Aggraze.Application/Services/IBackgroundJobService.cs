namespace Aggraze.Application.Services;

public interface IBackgroundJobService
{
    void EnqueueFileProcessing(string filePath, string sheetName);
}