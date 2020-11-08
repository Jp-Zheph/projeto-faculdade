using NewSIGASE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Data.Repositories.Interfaces
{
    public interface IEquipamentoRepository
    {
        IQueryable<Equipamento> Obter();

        Task<Equipamento> ObterAsync(Guid id);

        Task<Equipamento> ObterAsync(string serial);

        Task CriarAsync(Equipamento equipamento);

        Task EditarAsync(Equipamento equipamento);

        Task DeletarAsync(Equipamento equipamento);
    }
}
