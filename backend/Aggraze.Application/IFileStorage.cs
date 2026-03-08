namespace Aggraze.Application;

public interface IFileStorage
{
    Task<string> SaveAsync(Stream stream, string fileName, CancellationToken cancellationToken);
    Task<Stream> OpenReadAsync(string filePath, CancellationToken cancellationToken);
}