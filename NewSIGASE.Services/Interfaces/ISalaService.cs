using NewSIGASE.Dto.Request;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Services.Interfaces
{
    public interface ISalaService : INotifiable
    {

        IQueryable<Sala> Obter();

        IQueryable<Sala> ObterSomenteAtivos();

        IQueryable<Sala> Obter(EnumTipoSala tipo);

        Task<Sala> ObterAsync(Guid id);

        Task CriarAsync(SalaDto dto);

        Task EditarAsync(SalaDto dto);

        Task DeletarAsync(Guid id);
    }
}
