using NewSIGASE.Dto.Request;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Services.Interfaces
{
    public interface IUsuarioService : INotifiable
    {
        IQueryable<Usuario> Obter();
        Task<Usuario> Obter(Guid id);
        Task Criar(UsuarioDto usuarioDto);
        Task Editar(UsuarioDto dto);
        Task CriarSenha(SenhaCriarDto senhaDto);
        Usuario ValidarLogin(string email, string password, out string mesangem);
        Task Deletar(Guid id);

        IQueryable<Usuario> ObterPorPerfil(EnumTipoPerfil perfil);
    }
}
