
using Microsoft.EntityFrameworkCore;
using NewSIGASE.Models;
using Toolbelt.ComponentModel.DataAnnotations;

namespace NewSIGASE.Data
{
    public class SIGASEContext : DbContext
    {
        public SIGASEContext (DbContextOptions<SIGASEContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.BuildIndexesFromAnnotations();

            modelBuilder.Entity<Sala>().HasQueryFilter(s => s.Ativo);

            // Cria um relacionamento do tipo muitos-para-muitos 
            // entre as entidades Sala e Equipamento
            modelBuilder.Entity<SalaEquipamento>()
                .HasKey(s => new { s.SalaId, s.EquipamentoId });

            modelBuilder.Entity<SalaEquipamento>()
                .HasOne(s => s.Sala)
                .WithMany(s => s.SalaEquipamentos)
                .HasForeignKey(s => s.SalaId);

            modelBuilder.Entity<SalaEquipamento>()
                .HasOne(s => s.Equipamento)
                .WithOne(s => s.SalaEquipamento)
                .HasForeignKey<SalaEquipamento>(s => s.EquipamentoId);
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
		public DbSet<SalaEquipamento> SalaEquipamentos { get; set; }
		public DbSet<Sala> Salas { get; set; }
		public DbSet<Agendamento> Agendamentos { get; set; }
		public DbSet<Endereco> Enderecos { get; set; }
	}
}
