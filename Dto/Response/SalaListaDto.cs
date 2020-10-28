﻿using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.Linq;

namespace NewSIGASE.Dto.Response
{
	public class SalaListaDto
    {

		public Guid Id { get; set; }
		public EnumTipoSala Tipo { get; set; }
		public string IdentificadorSala { get; set; }
		public int CapacidadeAlunos { get; set; }
		public int QuantidadeEquipamentos { get; set; }

		public SalaListaDto(Sala sala)
		{
			Id = sala.Id;
			Tipo = sala.Tipo;
			IdentificadorSala = sala.IdentificadorSala;
			CapacidadeAlunos = sala.CapacidadeAlunos;
			QuantidadeEquipamentos = sala.Equipamentos.Count();
		}
	}
	
}
