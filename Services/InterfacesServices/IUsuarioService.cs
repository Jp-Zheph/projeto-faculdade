using NewSIGASE.Dto.Request;
using SIGASE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Services.InterfacesServices
{
    public interface IUsuarioService : INotifiable
    {
        IQueryable<Usuario> Obter();
        Task<Usuario> Obter(Guid id);
        Task Criar(UsuarioDto usuarioDto);
        Task Editar(UsuarioDto dto);
        Task CriarSenha(Guid usuarioId, string senha);
    }
}
