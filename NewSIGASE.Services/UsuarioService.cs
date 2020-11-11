using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Data.Repositories.Interfaces;
using NewSIGASE.Dto.Request;
using NewSIGASE.Services.Constantes;
using NewSIGASE.Services.Interfaces;
using NewSIGASE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using NewSIGASE.Models.Enum;
using System.Collections.Generic;

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
            var usuario = await _usuarioRepository.ObterAsync(id);
            if (usuario == null)
            {
                AddNotification("ObterUsuario", MensagemValidacao.Usuario.NaoExiste);
                return null;
            }
            return usuario;
        }

        public async Task Criar(UsuarioDto usuarioDto)
        {
            var usuarioCadastrado = await _usuarioRepository.ObterAsync(usuarioDto.Email, usuarioDto.Matricula, usuarioDto.Documento);

            ValidarUsuarioCadastrado(usuarioDto, usuarioCadastrado);
            if (Invalid)
            {
                return;
            }

            var endereco = new Endereco(usuarioDto.Endereco.Logradouro, usuarioDto.Endereco.Numero, usuarioDto.Endereco.Bairro, usuarioDto.Endereco.Cep, 
                usuarioDto.Endereco.Cidade, usuarioDto.Endereco.UF, usuarioDto.Endereco.Complemento, usuarioDto.Endereco.PontoReferencia);

            var usuario = new Usuario(usuarioDto.Matricula, usuarioDto.Email, usuarioDto.Nome, usuarioDto.Perfil, endereco, usuarioDto.Telefone, usuarioDto.DataNascimento, usuarioDto.Documento);
            
            try
            {
                await _usuarioRepository.CriarAsync(usuario);
            }
            catch(Exception ex)
            {
                AddNotification("CadastrarUsuario", MensagemValidacao.ContacteSuporte);
                return;
            }
            

            _emailService.AdicionarDestinatario(usuario.Email, usuario.Nome);
            await _emailService.EnviarEmailCadastroUsuario(usuario);
        }

        public async Task Deletar(Guid id)
        {
            var usuario = await _usuarioRepository.ObterAsync(id);

            AddNotifications(new Contract()
                .IsNotNull(usuario, "ExcluirUsuario", MensagemValidacao.Usuario.NaoExiste)
                .IfNotNull(usuario, x => x.IsNull(usuario.Agendamentos, "ExcluirUsuario", "Não é possivel excluir, pois usuário possui agendamentos vinculados a ele."))
            );

            if (Invalid)
            {
                return;
            }

            await _usuarioRepository.DeletarAsync(usuario);
        }

        public void ValidarUsuarioCadastrado(UsuarioDto usuarioDto, Usuario usuarioCadastrado)
        {
            AddNotifications(new Contract()
                .IfNotNull(usuarioCadastrado?.Email, x => x.AreNotEquals(usuarioCadastrado.Email, usuarioDto.Email, "CriarUsuario", MensagemValidacao.Usuario.JaCadastrado(nameof(usuarioDto.Email)), StringComparison.OrdinalIgnoreCase))
                .IfNotNull(usuarioCadastrado?.Matricula, x => x.AreNotEquals(usuarioCadastrado.Matricula, usuarioDto.Matricula, "CriarUsuario", MensagemValidacao.Usuario.JaCadastrado(nameof(usuarioDto.Matricula)), StringComparison.OrdinalIgnoreCase))
                .IfNotNull(usuarioCadastrado?.Documento, x => x.AreNotEquals(usuarioCadastrado.Documento, usuarioDto.Documento, "CriarUsuario", MensagemValidacao.Usuario.JaCadastrado("CPF"), StringComparison.OrdinalIgnoreCase))
            );
        }

        public async Task Editar(UsuarioDto dto)
        {
            var usuarioEditar = await _usuarioRepository.ObterAsync(dto.Id.Value);

            await ValidarUsuarioEditar(usuarioEditar, dto.Email, dto.Matricula, dto.Documento);
            if (Invalid)
            {
                return;
            }

            usuarioEditar.Editar(dto.Matricula, dto.Email, dto.Nome, dto.Perfil, dto.Ativo, dto.Telefone, dto.DataNascimento, dto.Documento);
            usuarioEditar.Endereco.Editar(dto.Endereco.Logradouro, dto.Endereco.Numero, dto.Endereco.Bairro, dto.Endereco.Cep, dto.Endereco.Cidade, 
                dto.Endereco.UF, dto.Endereco.Complemento, dto.Endereco.PontoReferencia);

            await _usuarioRepository.EditarEnderecoAsync(usuarioEditar.Endereco);
            await _usuarioRepository.EditarAsync(usuarioEditar);
        }

        private async Task ValidarUsuarioEditar(Usuario usuarioEditar, string email, string matricula, string documento)
        {
            if (usuarioEditar == null)
            {
                AddNotification("UsuarioEditar", MensagemValidacao.Usuario.NaoExiste);
                return;
            }

            var usuarioDuplicado = await _usuarioRepository.ObterAsync(email, matricula, documento);
            if (usuarioDuplicado != null && usuarioDuplicado.Id != usuarioEditar.Id)
            {
                AddNotification("UsuarioEditar", MensagemValidacao.Usuario.JaCadastrado("Usuário"));
                return;
            }
        }

        public async Task CriarSenha(SenhaCriarDto senhaDto)
        {
            var usuario = await _usuarioRepository.ObterAsync(senhaDto.Id);
            if (usuario == null)
            {
                AddNotification("AlterarSenha", MensagemValidacao.Usuario.NaoExiste);
                return;
            }

            AddNotifications(new Contract()
                .AreNotEquals(senhaDto.SenhaNova, usuario.Matricula, "AlterarSenha", MensagemValidacao.Usuario.SenhaNaoPodeSerMatricula, StringComparison.OrdinalIgnoreCase)
                .AreEquals(senhaDto.SenhaAtual, usuario.Senha, "AlterarSenha", MensagemValidacao.Usuario.SenhaDiferente, StringComparison.OrdinalIgnoreCase)
            );

            if (Invalid)
            {
                return;
            }

            usuario.AlterarSenha(senhaDto.SenhaNova);
            await _usuarioRepository.EditarAsync(usuario);
        }

        public Usuario ValidarLogin(string email, string senha, out string mensagem)
        {
            mensagem = "";
            try
            {
                var retorno = _usuarioRepository.ObterPorEmailAsync(email).GetAwaiter().GetResult();
                if (retorno != null && retorno.Senha == senha)
                {
                    return retorno;
                }
                else if (retorno != null && retorno.Senha != senha)
                {
                    mensagem = "Senha informada incorreta para o respectivo usuário";
                    return null;
                }
                else if (retorno == null)
                {
                    mensagem = "Nenhum usuário foi encontrado para o E-mail informado.";
                    return null;
                }

            }catch(Exception ex)
            {
                mensagem = "<strong>Erro ao conectar ao sistema contacte o suporte .</strong>";
                return null;
            }
            return null;
        }

        public IEnumerable<Usuario> ObterPorPerfil(EnumTipoPerfil perfil)
        {
            return _usuarioRepository.ObterPorPerfil(perfil);
        }

        public async Task RecuperarSenha(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                AddNotification("RecuperarSenha", MensagemValidacao.CampoObrigatorio);
                return;
            }

            var usuario = await _usuarioRepository.ObterAsync(email);
            if (usuario == null)
            {
                AddNotification("RecuperarSenha", MensagemValidacao.Usuario.NaoExiste);
                return;
            }

            usuario.AlterarSenha();
            await _usuarioRepository.EditarAsync(usuario);

            _emailService.AdicionarDestinatario(usuario.Email, usuario.Nome);
            await _emailService.EnviarEmailRecuperarSenha(usuario);
        }
    }
}
