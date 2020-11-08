using NewSIGASE.Dto.Response.IntegracaoCorreios;
using System.Threading.Tasks;

namespace NewSIGASE.Services.Interfaces
{
    public interface ICepService : INotifiable
    {
        Task<ConsultaCepDto> ConsultarCepAsync(string cep);
    }
}
