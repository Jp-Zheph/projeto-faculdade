using Microsoft.EntityFrameworkCore;
using NewSIGASE.Data.Repositories.Interfaces;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Data.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly SIGASEContext _context;

        public AgendamentoRepository(SIGASEContext context)
        {
            _context = context;
        }

        public IQueryable<Agendamento> Obter()
        {
            var agendamentos = _context.Agendamentos
                .IgnoreQueryFilters()
                    .Include(a => a.Sala)
                    .Include(a => a.Usuario)
                  .AsNoTracking().OrderByDescending(a => a.DataAgendada);

            if (agendamentos == null)
            {
                return Array.Empty<Agendamento>().AsQueryable();
            }

            return agendamentos;
        }

        public IQueryable<Agendamento> ObterPorUsuario(Guid usuarioId)
        {
            var agendamentos = _context.Agendamentos
                    .Include(a => a.Sala)
                    .Include(a => a.Usuario)
                    .Where(a => a.UsuarioId == usuarioId)
                    .AsNoTracking().OrderByDescending(a => a.DataAgendada);

            if (agendamentos == null)
            {
                return Array.Empty<Agendamento>().AsQueryable();
            }

            return agendamentos;
        }

        public IQueryable<Agendamento> ObterPorData(DateTime dataInicio, DateTime dataFim)
        {
            var agendamentos = _context.Agendamentos
                .Include(a => a.Usuario)
                .Include(a => a.Sala)
                    .ThenInclude(s => s.SalaEquipamentos)
                        .ThenInclude(s => s.Equipamento)
                .Where(a => a.DataAgendada >= dataInicio && a.DataAgendada <= dataFim)
                .AsNoTracking();

            if (agendamentos == null)
            {
                return Array.Empty<Agendamento>().AsQueryable();
            }

            return agendamentos;
        }

        public async Task<Agendamento> ObterAsync(Guid id)
        {
            return await _context.Agendamentos
                .Include(a => a.Sala)
                .IgnoreQueryFilters()
                .Include(a => a.Usuario)
                   .ThenInclude(u => u.Endereco)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Agendamento> EditarAsync(Agendamento agendamento)
        {
            _context.Entry<Agendamento>(agendamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return agendamento;
        }

        public async Task<Agendamento> ObterAsync(Guid salaId, EnumPeriodo periodo, DateTime data)
        {
            return await _context.Agendamentos
                .Where(a => a.SalaId == salaId && a.Periodo == periodo && a.DataAgendada.Date == data.Date)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Agendamento> ObterAsync(EnumPeriodo periodo, DateTime data)
        {
            return await _context.Agendamentos
                .Where(a => a.Periodo == periodo && a.DataAgendada.Date == data.Date)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task CriarAsync(Agendamento agendamento)
        {
            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
        }


    }
}
