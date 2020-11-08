using System;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace NewSIGASE.Models
{
	public class SalaEquipamento
	{
        public Sala Sala { get; set; }

		[Index("UN_SalaEquipamento_SalaId", 0, IsUnique = false)]
		public Guid SalaId { get; set; }

        public Equipamento Equipamento { get; set; }

		[Index("UN_SalaEquipamento_EquipamentoId", 0, IsUnique = false)]
		public Guid EquipamentoId { get; set; }

		public DateTime DataCriacao { get; set; }

		public SalaEquipamento(Guid salaId, Guid equipamentoId)
		{
			SalaId = salaId;
			EquipamentoId = equipamentoId;
			DataCriacao = DateTime.Now;
		}
	}
}
