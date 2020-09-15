using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewSIGASE.Models.Enum;

namespace NewSIGASE.Models

{
	public class Sala
	{
		public int Id { get; set; }
		public EnumTipoSala Tipo { get; set; }
		public string IdentificadorSala { get; set; }
		public string Observacao { get; set; }
		public int CapacidadeAlunos { get; set; }
		public List<SalaEquipamento> SalaEquipamento { get; set; }
		public List<Agendamento> Agendamento { get; set; }

	
	}
}
