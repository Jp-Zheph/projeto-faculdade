using Microsoft.EntityFrameworkCore;
using NewSIGASE.Data.Repositories.Interfaces;
using NewSIGASE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Data.Repositories
{
    public class EquipamentoRepository : IEquipamentoRepository
    {
        private readonly SIGASEContext _context;

        public EquipamentoRepository(SIGASEContext context)
        {
            _context = context;
        }

        public IQueryable<Equipamento> Obter()
        {
            var equipamentos = _context.Equipamentos
                .AsNoTracking();

            if (equipamentos == null)
            {
                return Array.Empty<Equipamento>().AsQueryable();
            }

            return equipamentos;
        }

        public async Task<Equipamento> ObterAsync(Guid id)
        {
            return await _context.Equipamentos
                .Include(e => e.SalaEquipamento)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Equipamento> ObterAsync(string serial)
        {
            return await _context.Equipamentos
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Serial == serial);
        }

        public async Task CriarAsync(Equipamento equipamento)
        {
            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();
        }

        public async Task EditarAsync(Equipamento equipamento)
        {
            _context.Entry<Equipamento>(equipamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Equipamento equipamento)
        {
            _context.Equipamentos.Remove(equipamento);
            await _context.SaveChangesAsync();
        }
    }
}
