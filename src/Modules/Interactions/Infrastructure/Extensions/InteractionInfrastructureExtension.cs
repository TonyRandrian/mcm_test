using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Infrastructure.Database;
using Mcm.Interactions.Infrastructure.Repositories;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Services;
using Mcm.Shared.Infrastructure.Autorisations;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Interactions.Infrastructure.Extensions
{
    public static class InteractionInfrastructureExtension
    {
        public static IServiceCollection AddInteractionInfrastructure(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<InteractionDbContext>(options => 
                options.UseNpgsql(conf.GetConnectionString("DefaultConnection"), sql => 
                {
                    sql.MigrationsAssembly(typeof(InteractionDbContext).Assembly.FullName);
                    sql.MigrationsHistoryTable("__EFMigrationHistory", "interactions");  
                }));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  
            services.AddScoped<IInteractionUow, InteractionUow>();
            services.AddScoped<IInteractionRepository, InteractionRepository>();
            services.AddScoped<IInteractionTypeRepository, InteractionTypeRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ITypeFieldRepository, TypeFieldRepository>();
            services.AddScoped<ITenantProvider, TenantProvider>();
            
            return services;
        }
    }
}