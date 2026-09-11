using System.Reflection;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Shared.Application.Extensions
{
    public static class SharedApplicationServiceExtension
    {
        public static IServiceCollection AddSharedApplication(this IServiceCollection services, IConfiguration config)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg
                => cfg.RegisterServicesFromAssembly(assembly));

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IResourceService, ResourceService>();
            services.Configure<SmtpSettings>(
                config.GetSection("SmtpSettings"));
            services.AddScoped<IMailService, MailService>();

            return services;
        }
    }
}