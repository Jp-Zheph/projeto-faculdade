using NewSIGASE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Data.Repositories.Interfaces
{
    public interface IEquipamentoRepository
    {
        IQueryable<Equipamento> Obter();

        IQueryable<Equipamento> ObterSemSala();

        Task<Equipamento> ObterAsync(Guid id);

        Task<Equipamento> ObterAsync(string serial);

        Task CriarAsync(Equipamento equipamento);

        Task EditarAsync(Equipamento equipamento);

        Task DeletarAsync(Equipamento equipamento);

        Task<IEnumerable<Equipamento>> ObterAsync(IEnumerable<Guid> ids);
    }
}
