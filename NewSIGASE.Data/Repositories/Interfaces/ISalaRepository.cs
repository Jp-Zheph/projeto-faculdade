using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSIGASE.Data.Repositories.Interfaces
{
    public interface ISalaRepository
    {
        IQueryable<Sala> Obter();

        IQueryable<Sala> ObterSomenteAtivos();

        IQueryable<Sala> Obter(EnumTipoSala tipo);

        Task<Sala> ObterAsync(string identificadorSala);

        Task CriarAsync(Sala sala);

        Task<Sala> ObterAsync(Guid id);

        Task EditatAsync(Sala sala);

        Task DeletarSalaEquipamentosAsync(IEnumerable<SalaEquipamento> salaEquipamentos);

        Task CriarSalaEquipamentosAsync(IEnumerable<SalaEquipamento> salaEquipamentos);

        Task DeletarAsync(Sala sala);
    }
}
