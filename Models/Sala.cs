using System;
using System.Collections.Generic;
using NewSIGASE.Models.Enum;

namespace NewSIGASE.Models
{
	public class Sala
	{
		public Guid Id { get; set; }
		public EnumTipoSala Tipo { get; set; }
		public string IdentificadorSala { get; set; }
		public string Observacao { get; set; }
		public int CapacidadeAlunos { get; set; }

		public List<Equipamento> Equipamentos { get; set; }
		public List<Agendamento> Agendamentos { get; set; }

		public Sala(EnumTipoSala tipo, 
			string identificadorSala, 
			string observacao, 
			int capacidadeAlunos)
		{
			Id = Guid.NewGuid();
			Tipo = tipo;
			IdentificadorSala = identificadorSala;
			Observacao = observacao;
			CapacidadeAlunos = capacidadeAlunos;
			Agendamentos = new List<Agendamento>();
			Equipamentos = new List<Equipamento>();
		}

		public Sala()
		{ }

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
