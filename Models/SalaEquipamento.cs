using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NewSIGASE.Models.Enum;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace NewSIGASE.Models
{
	public class SalaEquipamento
	{
		public Guid Id { get; set; }

        public Sala Sala { get; set; }
        public Guid SalaId { get; set; }

        public Equipamento Equipamento { get; set; }
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
