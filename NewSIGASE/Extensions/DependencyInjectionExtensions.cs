using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewSIGASE.Data.Repositories;
using NewSIGASE.Data.Repositories.Interfaces;
using NewSIGASE.Services;
using NewSIGASE.Services.Interfaces;
using NewSIGASE.Data;

namespace NewSIGASE.Infra.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SIGASEContext>(options => options.UseSqlServer(configuration["StringConexao:SIGASEContext"]));

            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAgendamentoService, AgendamentoService>();
            services.AddTransient<IEquipamentoService, EquipamentoService>();
            services.AddTransient<ISalaService, SalaService>();
            services.AddTransient<ICepService, CepService>();

            services.AddTransient<IUsuarioRespository, UsuarioRepository>();
            services.AddTransient<IAgendamentoRepository, AgendamentoRepository>();
            services.AddTransient<IEquipamentoRepository, EquipamentoRepository>();
            services.AddTransient<ISalaRepository, SalaRepository>();

            return services;
        }
    }
}
