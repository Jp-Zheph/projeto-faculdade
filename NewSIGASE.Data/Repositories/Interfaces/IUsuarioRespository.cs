
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Data.Repositories.Interfaces
{
    public interface IUsuarioRespository
    {
        IQueryable<Usuario> Obter();

        Task<Usuario> ObterAsync(Guid id);

        Task<Usuario> ObterAsync(string email, string matricula);

        Task CriarAsync(Usuario usuario);

        Task<Usuario> EditarAsync(Usuario usuario);

        Task<Usuario> ObterPorEmailAsync(string email);

        Task DeletarAsync(Usuario usuario);

        Task EditarEnderecoAsync(Endereco endereco);

        IEnumerable<Usuario> ObterPorPerfil(EnumTipoPerfil perfil);

        Task<Usuario> ObterAsync(string email);
    }
}
