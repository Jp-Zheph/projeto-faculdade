using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using NewSIGASE.Infra.Configuration;
using NewSIGASE.Models;
using NewSIGASE.Services.Constantes;
using NewSIGASE.Services.InterfacesServices;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NewSIGASE.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridClient _client;
        private readonly string _caminhoPastaEmails;
        private readonly string _urlLogin;

        public SendGridMessage Mensagem { get; private set; }

        public EmailService(IOptions<EmailOptions> options, IHostingEnvironment env)
        {
            _client = new SendGridClient(options.Value.ApiKey);
            _urlLogin = options.Value.UrlLogin;

            _caminhoPastaEmails = env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + CaminhoArquivoEmail.PastaEmailTemplates
                            + Path.DirectorySeparatorChar.ToString();

            Mensagem = new SendGridMessage();
            Mensagem.SetFrom(new EmailAddress(options.Value.EmailRemetente, options.Value.NomeRemetente));
        }

        public void AdicionarDestinatario(string email, string nome)
        {
            Mensagem.AddTo(email, nome);
        }

        public async Task EnviarEmailCadastroUsuario(Usuario usuario)
        {
            var caminhoTemplate = _caminhoPastaEmails + CaminhoArquivoEmail.ArquivoCadastroUsuario;

            var builder = new StringBuilder();
            builder.Append(File.ReadAllText(caminhoTemplate, Encoding.UTF8));

            builder.Replace("{{URL_ACESSO}}", _urlLogin);
            builder.Replace("{{EMAIL_USUARIO}}", usuario.Email);
            builder.Replace("{{SENHA_USUARIO}}", usuario.Senha);

            await EnviarAsync(AssuntosEmail.ConcluirCadastro, builder.ToString());
        }

        private async Task<bool> EnviarAsync(string assunto, string mensagem)
        {
            Mensagem.SetSubject(assunto);
            Mensagem.AddContent(MimeType.Html, mensagem);

            var response = await _client.SendEmailAsync(Mensagem);
            return response.StatusCode == System.Net.HttpStatusCode.Created;
        }
    }
}
