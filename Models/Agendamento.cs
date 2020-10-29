using NewSIGASE.Models.Enum;
using NewSIGASE.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Models
{
	public class Agendamento
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime DataCriacao { get; set; }
		public DateTime DataAgendada { get; set; }
        public EnumPeriodo Periodo { get; set; }
        public bool Status { get; set; }

		public Sala Sala { get; set; }
		public Guid SalaId { get; set; }

		public Usuario Usuario { get; set; }
		public Guid UsuarioId { get; set; }

        public Agendamento(DateTime dataAgendada, 
			EnumPeriodo periodo, 
			Guid salaId, 
			Guid usuarioId)
        {
			Id = Guid.NewGuid();
			DataCriacao = DateTime.UtcNow;
			DataAgendada = dataAgendada;
            Periodo = periodo;
            Status = true;
            SalaId = salaId;
            UsuarioId = usuarioId;
        }

		public Agendamento()
		{ }

		public void Editar(DateTime dataAgendada, 
			EnumPeriodo periodo,
			Guid salaId)
        {
			DataAgendada = dataAgendada;
			Periodo = periodo;
			SalaId = salaId;
		}
	}
}
