
using NewSIGASE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Data.Repositories.InterfacesRepositories
{
    public interface IUsuarioRespository
    {
        IQueryable<Usuario> Obter();

        Task<Usuario> Obter(Guid id);

        Usuario Obter(string email, string matricula);

        Task Criar(Usuario usuario);

         Task<Usuario> Editar(Usuario usuario);

        Usuario ObterPorEmail(string email);

    }
}
