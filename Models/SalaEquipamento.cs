using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Models
{
	public class SalaEquipamento
	{
		public int Id { get; set; }
		public int IdSala { get; set; }
		public Sala Sala { get; set; }
		public Equipamento Equipamento { get; set; }
		public int IdEquipamento { get; set; }

		
	}
}
