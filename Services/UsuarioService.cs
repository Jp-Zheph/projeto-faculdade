
using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Data.Repositories;
using NewSIGASE.Dto.Request;
using SIGASE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Services
{
    public class UsuarioService : Notifiable
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IQueryable<Usuario> Obter()
        {
            return _usuarioRepository.Obter();
        }

        public async Task<Usuario> Obter(Guid id)
        {
            return await _usuarioRepository.Obter(id);
        }

        public async Task Criar(UsuarioCriarDto usuarioDto)
        {
            var usuarioCadastrado = _usuarioRepository.Obter(usuarioDto.Email, usuarioDto.Matricula);

            ValidarUsuarioCadastrado(usuarioDto, usuarioCadastrado);
            if (Invalid)
            {
                return;
            }

            var usuario = new Usuario(usuarioDto.Matricula, usuarioDto.Email, usuarioDto.Nome, usuarioDto.Perfil);

            await _usuarioRepository.Criar(usuario);
        }

        public void ValidarUsuarioCadastrado(UsuarioCriarDto usuarioDto, Usuario usuarioCadastrado)
        {
            AddNotifications(new Contract()
                .IfNotNull(usuarioCadastrado?.Email, x => x.AreNotEquals(usuarioCadastrado.Email, usuarioDto.Email, "CriarUsuario", "Email já cadastrado.", StringComparison.OrdinalIgnoreCase))
                .IfNotNull(usuarioCadastrado?.Matricula, x => x.AreNotEquals(usuarioCadastrado.Matricula, usuarioDto.Matricula, "CriarUsuario", "Matrícula já cadastrado.", StringComparison.OrdinalIgnoreCase))
            );
        }

        public Usuario ValidarLogin(string email, string senha, out string mensagem)
        {
            mensagem = "";
            var retorno = _usuarioRepository.ObterPorEmail(email);
            if(retorno != null && retorno.Senha == senha){
                return retorno;
            }else if ( retorno != null && retorno.Senha != senha)
            {
                mensagem = "Senha informada incorreta para o usuário informado";
                return null;
            }else if(retorno == null)
            {
                mensagem = "Usuário informado não localizado.";
                return null;
            }
            return null;
        }

    }
}
