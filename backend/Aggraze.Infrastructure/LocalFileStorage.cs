using Aggraze.Application;

namespace Aggraze.Infrastructure;

public class LocalFileStorage : IFileStorage
{
    private readonly string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

    public async Task<string> SaveAsync(Stream stream, string fileName, CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(this.rootPath);

        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        var fullPath = Path.Combine(this.rootPath, uniqueFileName);

        await using var fileStreamOutput = new FileStream(fullPath, FileMode.Create);
        await stream.CopyToAsync(fileStreamOutput, cancellationToken);

        return uniqueFileName;
    }

    public Task<Stream> OpenReadAsync(string filePath, CancellationToken cancellationToken)
    {
        var fullPath = Path.Combine(this.rootPath, filePath);
        Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        return Task.FromResult(stream);
    }
}