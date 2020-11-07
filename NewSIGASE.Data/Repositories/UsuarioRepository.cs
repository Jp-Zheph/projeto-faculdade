
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
    public class UsuarioRepository : IUsuarioRespository
    {
        private readonly SIGASEContext _context;

        public UsuarioRepository(SIGASEContext context)
        {
            _context = context;
        }

        public IQueryable<Usuario> Obter()
        {
            var usuarios = _context.Usuarios
                .Include(u => u.Endereco)
                .AsNoTracking();

            if (usuarios == null)
            {
                return Array.Empty<Usuario>().AsQueryable();
            }

            return usuarios;
        }

        public IEnumerable<Usuario> ObterPorPerfil(EnumTipoPerfil perfil)
        {
            var usuarios = _context.Usuarios
                .Include(u => u.Endereco)
                .Where(u => u.Perfil == perfil)
                .AsNoTracking()
                .ToList();

            if (usuarios == null)
            {
                return Array.Empty<Usuario>().ToList();
            }

            return usuarios;
        }

        public async Task<Usuario> ObterAsync(Guid id)
        {
            return await _context.Usuarios
                .Include(u => u.Agendamentos)
                .Include(u => u.Endereco)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> ObterAsync(string email, string matricula)
        {
            return await _context.Usuarios
                .Include(u => u.Endereco)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email || u.Matricula == matricula);
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .Include(u => u.Endereco)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CriarAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Usuario usuario)
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

        public async Task<Usuario> EditarAsync(Usuario usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task EditarEnderecoAsync(Endereco endereco)
        {
            _context.Update(endereco);
            await _context.SaveChangesAsync();
        }
    }
}
