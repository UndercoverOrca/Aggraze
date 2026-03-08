using Aggraze.Application.Services;
using Hangfire;

namespace Aggraze.Infrastructure.Services;

public class HangfireBackgroundJobService : IBackgroundJobService
{
    private readonly IBackgroundJobClient backgroundJobClient;

    public HangfireBackgroundJobService(IBackgroundJobClient backgroundJobClient)
    {
        this.backgroundJobClient = backgroundJobClient;
    }

    public void EnqueueFileProcessing(string filePath, string sheetName)
    {
        this.backgroundJobClient.Enqueue<IFileProcessingService>(service => service.ProcessAsync(filePath, sheetName));
    }
}