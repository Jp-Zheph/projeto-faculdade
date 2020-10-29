using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NewSIGASE.Models.Enum;

namespace NewSIGASE.Models
{
	public class Sala
	{
		
		[Required]
		[Key]
		public Guid Id { get; set; }

		[Required]
		public EnumTipoSala Tipo { get; set; }

		[Required]
		public string IdentificadorSala { get; set; }
		public string Observacao { get; set; }

		[Required]
		public int CapacidadeAlunos { get; set; }

		public List<Equipamento> Equipamentos { get; set; }
		public List<Agendamento> Agendamentos { get; set; }

		public Sala(EnumTipoSala tipo, 
			string identificadorSala, 
			string observacao, 
			int capacidadeAlunos,
			List<Equipamento> equipamentos)
		{
			Id = Guid.NewGuid();
			Tipo = tipo;
			IdentificadorSala = identificadorSala;
			Observacao = observacao;
			CapacidadeAlunos = capacidadeAlunos;
			Equipamentos = equipamentos;
		}

		public Sala()
		{ }

		public void Editar(EnumTipoSala tipo,
			string identificadorSala,
			string observacao,
			int capacidadeAlunos)
        {
			Tipo = tipo;
			IdentificadorSala = identificadorSala;
			Observacao = observacao;
			CapacidadeAlunos = capacidadeAlunos;
		}

		public void AdicionarEquipamentos(IEnumerable<Equipamento> equipamentosId)
		{
			foreach (var equipamento in equipamentosId)
			{
				if (!Equipamentos.Contains(equipamento))
				{
					Equipamentos.Add(equipamento);
				}
			}
		}
	}
}
