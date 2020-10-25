using NewSIGASE.Models.Enum;
using System;

namespace NewSIGASE.Dto.Response
{
	public class SalaListaDto
    {

		public Guid Id { get; set; }
		public EnumTipoSala Tipo { get; set; }
		public string IdentificadorSala { get; set; }
		public int CapacidadeAlunos { get; set; }

		public SalaListaDto(Guid id, EnumTipoSala tipo, string identificadorSala, string observacao, int capacidadeAlunos)
		{
			Id = id;
			Tipo = tipo;
			IdentificadorSala = identificadorSala;
			CapacidadeAlunos = capacidadeAlunos;
		}
	}
	
}
