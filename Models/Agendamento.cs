using NewSIGASE.Models.Enum;
using NewSIGASE.Models;
using System;

namespace NewSIGASE.Models
{
	public class Agendamento
	{
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
			bool status, 
			Guid salaId, 
			Guid usuarioId)
        {
			Id = Guid.NewGuid();
			DataCriacao = DateTime.UtcNow;
			DataAgendada = dataAgendada;
            Periodo = periodo;
            Status = status;
            SalaId = salaId;
            UsuarioId = usuarioId;
        }

		public Agendamento()
		{ }
	}
}
