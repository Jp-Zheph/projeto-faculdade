using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DbSet<SIGASE.Models.Usuario> Usuario { get; set; }

        public DbSet<NewSIGASE.Models.SalaEquipamento> SalaEquipamento { get; set; }

        public DbSet<NewSIGASE.Models.Equipamento> Equipamento { get; set; }

        public DbSet<NewSIGASE.Models.Sala> Sala { get; set; }

        public DbSet<NewSIGASE.Models.Agendamento> Agendamento { get; set; }
    }
}
