using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Infrastructure.Database;
using Mcm.Catalog.Infrastructure.Repositories;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Services;
using Mcm.Shared.Infrastructure.Autorisations;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Catalog.Infrastructure.Extensions
{
    public static class CatalogInfrastructureExtension
    {
        public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<CatalogDbContext>(options => 
                options.UseNpgsql(conf.GetConnectionString("DefaultConnection"), sql => 
                {
                    sql.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName);
                    sql.MigrationsHistoryTable("__EFMigrationHistory", "Catalog");  
                }));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  
            services.AddScoped<ICatalogUow, CatalogUow>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        
            return services;
        }
    }
}