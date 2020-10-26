
using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Data.Repositories.InterfacesRepositories;
using NewSIGASE.Dto.Request;
using NewSIGASE.Services.Constantes;
using NewSIGASE.Services.InterfacesServices;
using NewSIGASE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Services
{
    public class UsuarioService : Notifiable, IUsuarioService
    {
        private readonly IUsuarioRespository _usuarioRepository;
        private readonly IEmailService _emailService;

        public UsuarioService(IUsuarioRespository usuarioRepository,
            IEmailService emailService)
        {
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
        }

        public IQueryable<Usuario> Obter()
        {
            return _usuarioRepository.Obter();
        }

        public async Task<Usuario> Obter(Guid id)
        {
            var usuario = await _usuarioRepository.Obter(id);
            if (usuario == null)
            {
                AddNotification("Usuario", MensagemValidacaoService.Usuario.NaoExiste);
                return null;
            }
            return usuario;
        }

        public async Task Criar(UsuarioDto usuarioDto)
        {
            var usuarioCadastrado = _usuarioRepository.Obter(usuarioDto.Email, usuarioDto.Matricula);

            ValidarUsuarioCadastrado(usuarioDto, usuarioCadastrado);
            if (Invalid)
            {
                return;
            }

            var usuario = new Usuario(usuarioDto.Matricula, usuarioDto.Email, usuarioDto.Nome, usuarioDto.Perfil, false);

            await _usuarioRepository.Criar(usuario);

            _emailService.AdicionarDestinatario(usuario.Email, usuario.Nome);
            await _emailService.EnviarEmailCadastroUsuario();
        }

        public void ValidarUsuarioCadastrado(UsuarioDto usuarioDto, Usuario usuarioCadastrado)
        {
            AddNotifications(new Contract()
                .IfNotNull(usuarioCadastrado?.Email, x => x.AreNotEquals(usuarioCadastrado.Email, usuarioDto.Email, "CriarUsuario", MensagemValidacaoService.Usuario.JaCadastrado(nameof(usuarioDto.Email)), StringComparison.OrdinalIgnoreCase))
                .IfNotNull(usuarioCadastrado?.Matricula, x => x.AreNotEquals(usuarioCadastrado.Matricula, usuarioDto.Matricula, "CriarUsuario", MensagemValidacaoService.Usuario.JaCadastrado(nameof(usuarioDto.Matricula)), StringComparison.OrdinalIgnoreCase))
            );
        }

        public async Task Editar(UsuarioDto dto)
        {
            var usuarioEditar = await _usuarioRepository.Obter(dto.Id.Value);

            ValidarUsuarioEditar(usuarioEditar, dto.Email, dto.Matricula);
            if (Invalid)
            {
                return;
            }

            usuarioEditar.Nome = dto.Nome;
            usuarioEditar.Email = dto.Email;
            usuarioEditar.Matricula = dto.Matricula;
            usuarioEditar.Perfil = dto.Perfil;
            usuarioEditar.Ativo = dto.Ativo;

            await _usuarioRepository.Editar(usuarioEditar);
        }

        private void ValidarUsuarioEditar(Usuario usuarioEditar, string email, string matricula)
        {
            if (usuarioEditar == null)
            {
                AddNotification("UsuarioEditar", MensagemValidacaoService.Usuario.NaoExiste);
                return;
            }

            var usuarioDuplicado = _usuarioRepository.Obter(email, matricula);
            if (usuarioDuplicado != null && usuarioDuplicado.Id != usuarioEditar.Id)
            {
                AddNotification("UsuarioEditar", MensagemValidacaoService.Usuario.JaCadastrado("Usuário"));
                return;
            }
        }

        public async Task CriarSenha(Guid usuarioId, string senha)
        {
            var usuario = await _usuarioRepository.Obter(usuarioId);
            if (usuario == null)
            {
                AddNotification("CriarSenha", MensagemValidacaoService.Usuario.NaoExiste);
                return;
            }

            usuario.AlterarSenha(senha);
            await _usuarioRepository.Editar(usuario);
        }

        public Usuario ValidarLogin(string email, string senha, out string mensagem)
        {
            mensagem = "";
            var retorno = _usuarioRepository.ObterPorEmail(email);
            if (retorno != null && retorno.Senha == senha)
            {
                return retorno;
            }
            else if (retorno != null && retorno.Senha != senha)
            {
                mensagem = "Senha informada incorreta para o usuário informado";
                return null;
            }
            else if (retorno == null)
            {
                mensagem = "Nenhum usuário econtrado para o E-mail informado.";
                return null;
            }
            return null;
        }
    }
}
