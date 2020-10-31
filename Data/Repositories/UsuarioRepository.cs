
using Microsoft.EntityFrameworkCore;
using NewSIGASE.Data.Repositories.InterfacesRepositories;
using NewSIGASE.Models;
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
            return _context.Usuarios.AsNoTracking();
        }

        public async Task<Usuario> Obter(Guid id)
        {
            return await _context.Usuarios
                .Include(u => u.Agendamentos)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public Usuario Obter(string email, string matricula)
        {
            return _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(u => u.Email == email || u.Matricula == matricula);
        }

        public Usuario ObterPorEmail(string email)
        {
            return _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(u => u.Email == email);
        }

        public async Task Criar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<Usuario> Editar(Usuario usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
    }
}
