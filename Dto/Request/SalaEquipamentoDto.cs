using NewSIGASE.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Request
{
	public class SalaEquipamentoDto
	{

		public Guid Id { get; set; }
		public Guid? SalaId { get; set; }
		public Guid? EquipamentoId { get; set; }
		public DateTime DataCriacao { get; set; }

		public SalaEquipamentoDto(Guid id, Guid? salaId, Guid? equipamentoId, DateTime dataCriacao)
		{
			Id = id;
			SalaId = salaId;
			EquipamentoId = equipamentoId;
			DataCriacao = dataCriacao;
		}
	}
}
