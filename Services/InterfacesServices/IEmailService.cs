using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Services.InterfacesServices
{
    public interface IEmailService
    {
        void AdicionarDestinatario(string email, string nome);

        Task EnviarEmailCadastroUsuario();
    }
}
