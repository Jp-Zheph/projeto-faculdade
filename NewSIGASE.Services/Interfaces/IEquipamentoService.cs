using NewSIGASE.Dto.Request;
using NewSIGASE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSIGASE.Services.Interfaces
{
    public interface IEquipamentoService : INotifiable
    {
        IQueryable<Equipamento> Obter();

        IQueryable<Equipamento> ObterSemSala();

        Task<Equipamento> ObterAsync(Guid id);

        Task CriarAsync(EquipamentoDto dto);

        Task EditarAsync(EquipamentoDto dto);

        Task DeletarAsync(Guid id);
    }
}
