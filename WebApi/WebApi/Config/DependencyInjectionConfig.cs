using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Config;
using WebApi.Models;

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

            return services;
        }
    }
}
