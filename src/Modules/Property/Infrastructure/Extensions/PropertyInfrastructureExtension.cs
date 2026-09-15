using Mcm.Property.Application.Interfaces;
using Mcm.Property.Infrastructure.Database;
using Mcm.Property.Infrastructure.Repositories;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Services;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Property.Infrastructure.Extensions
{
    public static class PropertyInfrastructureExtension
    {
        public static IServiceCollection AddPropertyInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PropertyDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), sql => 
                {
                    sql.MigrationsAssembly(typeof(PropertyDbContext).Assembly.FullName);
                    sql.MigrationsHistoryTable("__EFMigrationHistory", "properties");  
                }));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategorySettingRepository, CategorySettingRepository>();
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<IPropertyUow, PropertyUow>();
            services.AddScoped<IPropertyModule, PropertyModule>();
            services.AddScoped<ICategoryModule, CategoryModule>();

            return services;
        }
    }
}