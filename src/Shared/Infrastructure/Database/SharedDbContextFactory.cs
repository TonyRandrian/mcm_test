using Mcm.Shared.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Mcm.Shared.Infrastructure.Database;

public class SharedDbContextFactory : IDesignTimeDbContextFactory<SharedDbContext>
{
    public SharedDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<SharedDbContext>();
        // Cherche appsettings.json dans le projet API

        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../../src/API");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionString 'DefaultConnection' not found");

        options.UseNpgsql(connectionString);

        return new SharedDbContext(options.Options);
    }
}