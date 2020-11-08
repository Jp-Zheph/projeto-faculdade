using Flunt.Notifications;
using NewSIGASE.Dto.Response.IntegracaoCorreios;
using NewSIGASE.Services.Constantes;
using NewSIGASE.Services.Interfaces;
using System.Threading.Tasks;
using WebServiceCep;

namespace NewSIGASE.Services
{
    public class CepService : Notifiable, ICepService
    { 
        private readonly AtendeClienteClient _atendeCliente = new AtendeClienteClient();

        public async Task<ConsultaCepDto> ConsultarCepAsync(string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                AddNotification("ConsultarCep", MensagemValidacao.CampoObrigatorio);
                return null;
            }

            try
            {
                var endereco = await _atendeCliente.consultaCEPAsync(cep);
                if (endereco?.@return == null)
                {
                    AddNotification("ConsultarCep", MensagemValidacao.Cep.NaoEncontrado);
                    return null;
                }

                return new ConsultaCepDto(endereco.@return.end, endereco.@return.bairro, endereco.@return.cep,
                    endereco.@return.cidade, endereco.@return.uf, endereco.@return.complemento2);
            }
            catch
            {
                AddNotification("ConsultarCep", MensagemValidacao.Cep.ErrorService);
                return null;
            }
            
        }

    }
}
