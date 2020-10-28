﻿using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NewSIGASE.Dto.Response;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NewSIGASE.Dto.Request
{
	public class SalaDto : Notifiable, IValidatable
	{
		public Guid? Id { get; set; }
		[Required]
		public EnumTipoSala Tipo { get; set; }
		[Required]
		public string IdentificadorSala { get; set; }  // numero da sala
		[Required]
		public string Observacao { get; set; }
		public int CapacidadeAlunos { get; set; }
		[Required]
		public IEnumerable<EquipamentoListaDto> Equipamentos { get; set; }

		public SalaDto(Sala sala)
		{
			Tipo = sala.Tipo;
			IdentificadorSala = sala.IdentificadorSala;
			CapacidadeAlunos = sala.CapacidadeAlunos;
			Equipamentos = sala.Equipamentos.Select(x => new EquipamentoListaDto(x)); 
		}

		public SalaDto()
		{
					
		}

		public void Validate()
		{
			var tipoSalaExiste = Enum.IsDefined(typeof(EnumTipoSala), Tipo);

			AddNotifications(new Contract()

			   .IsTrue(tipoSalaExiste, nameof(Tipo), MensagemValidacaoDto.CampoTipoInvalido(nameof(Tipo)))

				.IsNotNullOrEmpty(IdentificadorSala, nameof(IdentificadorSala), MensagemValidacaoDto.CampoObrigatorio)
				.HasMaxLengthIfNotNullOrEmpty(IdentificadorSala, 100, nameof(IdentificadorSala), MensagemValidacaoDto.CampoLimite50Caracteres)
				.IsTrue(CapacidadeAlunos > 0, nameof(CapacidadeAlunos), MensagemValidacaoDto.CampoTipoInvalido(nameof(CapacidadeAlunos)))
				//.IsTrue(Equipamentos.Count() > 0, nameof(Equipamentos), MensagemValidacaoDto.CampoTipoInvalido(nameof(Equipamentos)))

			);

		}
	}
}