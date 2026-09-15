using Mcm.Authorizations.Application.Features.EventHandler;
using Mcm.Authorizations.Application.Features.History.Hubs;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Infrastructure.Database;
using Mcm.Authorizations.Infrastructure.Repositories;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Services;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Authorizations.Infrastructure.Extensions
{
    public static class CompanyInfrastructureExtension
    {
        public static IServiceCollection AddAuthorizationInfrastructure(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<AuthorizationDbContext>(options => 
                options.UseNpgsql(conf.GetConnectionString("DefaultConnection"), sql => 
                {
                    sql.MigrationsAssembly(typeof(AuthorizationDbContext).Assembly.FullName);
                    sql.MigrationsHistoryTable("__EFMigrationHistory", "authorizations");  
                }));
                
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  
            services.AddScoped<IAuthorizationUow, AuthorizationUow>();
            services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<IPwdResetRepository, PasswordResetRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
            services.AddScoped<IActivityLogService, ActivityLogService>();
            // services.AddScoped<INotificationHandler<IDomainEvent>, ActivityLogHandler>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITeamMemberModule, TeamMemberModule>();
            services.AddScoped<IPermissionService, PermissionService>();
            
            return services;
        }
    }
}