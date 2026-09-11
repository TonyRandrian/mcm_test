using Mcm.Shared.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Mcm.Property.Infrastructure.Database;

public class PropertyDbContextFactory : IDesignTimeDbContextFactory<PropertyDbContext>
{
    public PropertyDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<PropertyDbContext>();
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

        return new PropertyDbContext(options.Options, new DesignTimeTenantProvider());
    }

    public class DesignTimeTenantProvider : ITenantProvider
    {
        public Guid? GetTenantId()
        {
            return null;
        }
    }
}