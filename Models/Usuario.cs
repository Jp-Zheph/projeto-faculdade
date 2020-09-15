using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewSIGASE.Models.Enum;

namespace SIGASE.Models
{
	public class Usuario
	{

		public int Id { get; set; }
		public string Matricula { get; set; }
		public string Email { get; set; }
		public string Nome { get; set; }
		public string Senha { get; set; }
		public EnumTipoPerfil Tipo { get; set; }
		
	}
}
