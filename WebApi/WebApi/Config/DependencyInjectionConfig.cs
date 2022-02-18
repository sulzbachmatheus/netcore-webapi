using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Config;
using WebApi.Data.Protocols;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Data.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new ServerConfig();
            configuration.Bind(config);
            var applicationContext = new ApplicationContext(config.MongoDB);
            var repo = new ApplicationRepository(applicationContext);
            services.AddSingleton<IApplicationRepository>(repo);
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
