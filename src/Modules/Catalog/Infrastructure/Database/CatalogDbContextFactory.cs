using Mcm.Catalog.Infrastructure.Database;
using Mcm.Shared.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Mcm.Catalog.Infrastructure.Database;

public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
{
    public CatalogDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<CatalogDbContext>();
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

        return new CatalogDbContext(options.Options, new DesignTimeTenantProvider());
    }

    public class DesignTimeTenantProvider : ITenantProvider
    {
        public Guid? GetTenantId()
        {
            return null;
        }
    }
}