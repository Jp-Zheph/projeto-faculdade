using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Data.Repositories.Interfaces
{
    public interface IAgendamentoRepository
    {
        IQueryable<Agendamento> Obter();

        IQueryable<Agendamento> ObterPorUsuario(Guid usuarioId);

        IQueryable<Agendamento> ObterPorData(DateTime dataInicio, DateTime dataFim);

        Task<Agendamento> ObterAsync(Guid id);

        Task<Agendamento> EditarAsync(Agendamento agendamento);

        Task<Agendamento> ObterAsync(Guid salaId, EnumPeriodo periodo, DateTime data);

        Task CriarAsync(Agendamento agendamento);
    }
}
