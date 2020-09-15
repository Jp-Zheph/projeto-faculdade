using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Models
{
	public class Agendamento
	{
		public int Id { get; set; }
		public int IdSala { get; set; }
		public DateTime Data { get; set; }
		public Sala Sala { get; set; }
		public int IdUsuario { get; set; }
		public bool Status { get; set; }
	}
}
