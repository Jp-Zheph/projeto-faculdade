using SIGASE.Models;
using System;

namespace NewSIGASE.Models
{
	public class Agendamento
	{
		public Guid Id { get; set; }
		public DateTime DataCriacao { get; set; }
		public DateTime DataInicial { get; set; }
		public DateTime DataFinal { get; set; }
		public bool Status { get; set; }

		public Sala Sala { get; set; }
		public Guid SalaId { get; set; }

		public Usuario Usuario { get; set; }
		public Guid UsuarioId { get; set; }

		public Agendamento(DateTime dataInicial,
			DateTime dataFinal, 
			bool status, 
			Guid salaId, 
			Guid usuarioId)
		{
			Id = Guid.NewGuid();
			DataCriacao = DateTime.UtcNow;
			DataInicial = dataInicial;
			DataFinal = dataFinal;
			Status = status;
			SalaId = salaId;
			UsuarioId = usuarioId;
		}

		public Agendamento()
		{ }
	}
}
