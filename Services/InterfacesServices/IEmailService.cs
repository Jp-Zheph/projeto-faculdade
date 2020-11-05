using NewSIGASE.Dto.Request;
using NewSIGASE.Models;
using System.Threading.Tasks;

namespace NewSIGASE.Services.InterfacesServices
{
    public interface IEmailService
    {
        void AdicionarDestinatario(string email, string nome);

        Task EnviarEmailCadastroUsuario(Usuario usuario);
        Task EnviarEmailAprovacaoAgendamento(Agendamento agendamento);
    }
}
