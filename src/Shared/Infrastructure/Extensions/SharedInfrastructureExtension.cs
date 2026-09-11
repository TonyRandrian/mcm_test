using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Services;
using Mcm.Shared.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Interceptors;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Shared.Infrastructure.Extensions
{
    public static class SharedInfrastructureExtension
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<SharedDbContext>(options =>
                options.UseNpgsql(conf.GetConnectionString("DefaultConnection"), sql => 
                {
                    sql.MigrationsAssembly(typeof(SharedDbContext).Assembly.FullName);
                    sql.MigrationsHistoryTable("__EFMigrationHistory", "Shared");  
                }));
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<TenantSaveChangesInterceptor>();


            
            return services;
        }
    }
}