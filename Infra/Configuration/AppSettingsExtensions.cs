using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NewSIGASE.Infra.Configuration
{
    public static class AppSettingsExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration);
            services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.Email));
            services.Configure<StringConexaoOptions>(configuration.GetSection(StringConexaoOptions.StringConexao));

            return services;
        }
    }
}
