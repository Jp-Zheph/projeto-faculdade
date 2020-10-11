
using Microsoft.EntityFrameworkCore;
using NewSIGASE.Data.Repositories.InterfacesRepositories;
using SIGASE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRespository
    {
        private readonly SIGASEContext _context;

        public UsuarioRepository(SIGASEContext context)
        {
            _context = context;
        }

        public IQueryable<Usuario> Obter()
        {
            return _context.Usuario.AsNoTracking();
        }

        public async Task<Usuario> Obter(Guid id)
        {
            return await _context.Usuario
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public Usuario Obter(string email, string matricula)
        {
            return _context.Usuario
                .AsNoTracking()
                .FirstOrDefault(u => u.Email == email || u.Matricula == matricula);
        }

        public async Task Criar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> Editar(Usuario usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
    }
}
