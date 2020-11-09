using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

		public DateTime DataCriacao { get; set; }
		public bool Ativo { get; set; }

		[Column(TypeName = "DECIMAL(18,3)")]
		public decimal Area { get; set; }
		public int Andar { get; set; }

		public List<SalaEquipamento> SalaEquipamentos { get; set; }

		public List<Agendamento> Agendamentos { get; set; }

		public Sala(EnumTipoSala tipo,
			string identificadorSala,
			string observacao,
			decimal area,
			int andar,
			int capacidadeAlunos)
		{
			Id = Guid.NewGuid();
			Tipo = tipo;
			IdentificadorSala = identificadorSala;
			Observacao = observacao;
			CapacidadeAlunos = capacidadeAlunos;
			DataCriacao = DateTime.Now;
			Ativo = true;
			Area = area;
			Andar = andar;
		}

		public Sala()
		{ }

		public void Editar(EnumTipoSala tipo,
			string identificadorSala,
			string observacao,
			decimal area,
			int andar,
			int capacidadeAlunos,
			bool ativo)
		{
			Tipo = tipo;
			IdentificadorSala = identificadorSala;
			Observacao = observacao;
			CapacidadeAlunos = capacidadeAlunos;
			Area = area;
			Andar = andar;
			Ativo = ativo;
		}

		public void AdicionarSalaEquipamento(List<SalaEquipamento> salaEquipamentos)
		{
			SalaEquipamentos = salaEquipamentos;
		}

		public void RemoverTodosSalaEquipamentos()
		{
			SalaEquipamentos = null;
		}

		public bool PermissaoDesativar()
		{
			return Agendamentos.All(a => a.DataAgendada.Date < DateTime.Now.Date);
		}
	}
}
