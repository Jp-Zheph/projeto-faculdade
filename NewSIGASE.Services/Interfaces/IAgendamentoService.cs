
using NewSIGASE.Dto.Request;
using NewSIGASE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Services.Interfaces
{
    public interface IAgendamentoService : INotifiable
    {
        IQueryable<Agendamento> Obter(string perfil, Guid usuarioId);

        Task<Agendamento> ObterAsync(Guid id);

        Task AprovarAgendamento(AgendamentoAprovacaoDto aprovacaoDto, Guid aprovadorId);

        Task CriarAsync(AgendamentoDto dto);

        IQueryable<Agendamento> GerarRelatorio(DateTime dataInicio, DateTime dataFim);
    }
}
