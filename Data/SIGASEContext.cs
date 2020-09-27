
using Microsoft.EntityFrameworkCore;

namespace SIGASE.Models
{
    public class SIGASEContext : DbContext
    {
        public SIGASEContext (DbContextOptions<SIGASEContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
