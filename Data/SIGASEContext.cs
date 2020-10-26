
using Microsoft.EntityFrameworkCore;
using NewSIGASE.Models;

namespace SIGASE.Models
{
    public class SIGASEContext : DbContext
    {
        public SIGASEContext (DbContextOptions<SIGASEContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Equipamento> Equipamentos { get; set; }

		public DbSet<Sala> Salas { get; set; }
	}
}
