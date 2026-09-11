using Mcm.Contacts.Application.Interfaces;
using Mcm.Contacts.Infrastructure.Database;
using Mcm.Contacts.Infrastructure.Repositories;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Services;
using Mcm.Shared.Infrastructure.Autorisations;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Contacts.Infrastructure.Extensions
{
    public static class ContactInfrastructureExtension
    {
        public static IServiceCollection AddContactInfrastructure(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<ContactDbContext>(options => 
                options.UseNpgsql(conf.GetConnectionString("DefaultConnection"), sql => 
                {
                    sql.MigrationsAssembly(typeof(ContactDbContext).Assembly.FullName);
                    sql.MigrationsHistoryTable("__EFMigrationHistory", "contacts");  
                }));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  
            services.AddScoped<IContactUow, ContactUow>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactModule, ContactModule>();
            services.AddScoped<ITenantProvider, TenantProvider>();
            
            return services;
        }
    }
}