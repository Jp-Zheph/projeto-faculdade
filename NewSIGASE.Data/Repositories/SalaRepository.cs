using Microsoft.EntityFrameworkCore;
using NewSIGASE.Data.Repositories.Interfaces;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Data.Repositories
{
    public class SalaRepository : ISalaRepository
    {
        private readonly SIGASEContext _context;

        public SalaRepository(SIGASEContext context)
        {
            _context = context;
        }

        public IQueryable<Sala> Obter()
        {
            var salas = _context.Salas.IgnoreQueryFilters()
                .Include(s => s.SalaEquipamentos)
                .AsNoTracking();

            if (salas == null)
            {
                return Array.Empty<Sala>().AsQueryable();
            }

            return salas;
        }

        public IQueryable<Sala> ObterSomenteAtivos()
        {
            var salas = _context.Salas
                .Include(s => s.SalaEquipamentos)
                .AsNoTracking();

            if (salas == null)
            {
                return Array.Empty<Sala>().AsQueryable();
            }

            return salas;
        }

        public IQueryable<Sala> Obter(EnumTipoSala tipo)
        {
            var salas = _context.Salas
                .Include(s => s.SalaEquipamentos)
                .Where(s => s.Tipo == tipo)
                .AsNoTracking();

            if (salas == null)
            {
                return Array.Empty<Sala>().AsQueryable();
            }

            return salas;
        }

        public async Task<Sala> ObterAsync(string identificadorSala)
        {
            return await _context.Salas
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.IdentificadorSala == identificadorSala);
        }

        public async Task CriarAsync(Sala sala)
        {
            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();
        }

        public async Task<Sala> ObterAsync(Guid id)
        {
            return await _context.Salas
                .Include(s => s.SalaEquipamentos)
                .Include(s => s.Agendamentos)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task EditatAsync(Sala sala)
        {
            _context.Entry<Sala>(sala).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletarSalaEquipamentosAsync(IEnumerable<SalaEquipamento> salaEquipamentos)
        {
            _context.SalaEquipamentos.RemoveRange(salaEquipamentos);
            await _context.SaveChangesAsync();
        }

        public async Task CriarSalaEquipamentosAsync(IEnumerable<SalaEquipamento> salaEquipamentos)
        {
            _context.SalaEquipamentos.AddRange(salaEquipamentos);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Sala sala)
        {
            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();
        }
    }
}
