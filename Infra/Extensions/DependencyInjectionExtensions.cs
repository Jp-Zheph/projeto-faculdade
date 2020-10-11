using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewSIGASE.Data.Repositories;
using NewSIGASE.Data.Repositories.InterfacesRepositories;
using NewSIGASE.Services;
using NewSIGASE.Services.InterfacesServices;
using SIGASE.Models;

namespace NewSIGASE.Infra.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SIGASEContext>(options => options.UseSqlServer(configuration["StringConexao:SIGASEContext"]));

            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<IUsuarioRespository, UsuarioRepository>();

            return services;
        }
    }
}
