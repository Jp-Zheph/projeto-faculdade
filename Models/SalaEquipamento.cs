using System;

namespace NewSIGASE.Models
{
	public class SalaEquipamento
	{
		public Guid Id { get; set; }
		public Sala Sala { get; set; }
		public Guid SalaId { get; set; }
		public Equipamento Equipamento { get; set; }
		public Guid EquipamentoId { get; set; }

		public SalaEquipamento(Guid salaId, 
			Guid equipamentoId)
		{
			Id = Guid.NewGuid();
			SalaId = salaId;
			EquipamentoId = equipamentoId;
		}
	}
}
