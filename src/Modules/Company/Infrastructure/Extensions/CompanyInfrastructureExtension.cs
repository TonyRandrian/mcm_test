using Mcm.Company.Application.Interfaces;
using Mcm.Company.Infrastructure.Database;
using Mcm.Company.Infrastructure.Repositories;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Services;
using Mcm.Shared.Infrastructure.Autorisations;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Company.Infrastructure.Extensions
{
    public static class CompanyInfrastructureExtension
    {
        public static IServiceCollection AddCompanyInfrastructure(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<CompanyDbContext>(options => 
                options.UseNpgsql(conf.GetConnectionString("DefaultConnection"), sql => 
                {
                    sql.MigrationsAssembly(typeof(CompanyDbContext).Assembly.FullName);
                    sql.MigrationsHistoryTable("__EFMigrationHistory", "Company");  
                }));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  
            services.AddScoped<ICompanyUow, CompanyUow>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            // services.AddScoped<ICompanyValueRepository, CompanyValueRepository>();
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<ICompanyModule, CompanyModule>();
            services.AddScoped<ITypeContactRepository, TypeContactRepository>();
            services.AddScoped<IActivitySectorRepository, ActivitySectorRepository>();
            
            return services;
        }
    }
}