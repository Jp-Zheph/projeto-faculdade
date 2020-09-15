using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Models
{
	public class Equipamento
	{
		public int Id { get; set; }
		public string Serial { get; set; }
		public string Nome { get; set; }
		public string Modelo { get; set; }

		public List<SalaEquipamento> SalaEquipamentos { get; set; }
	}
}
