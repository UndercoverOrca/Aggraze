using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Aggraze.Infrastructure;

public class AggrazeDbContextFactory : IDesignTimeDbContextFactory<AggrazeDbContext>
{
    public AggrazeDbContext CreateDbContext(string[] args)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var webApiDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..", "Aggraze.WebApi"));
        var configBasePath = Directory.Exists(webApiDirectory) ? webApiDirectory : currentDirectory;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(configBasePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<AggrazeDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AggrazeDbContext(optionsBuilder.Options);
    }
}
