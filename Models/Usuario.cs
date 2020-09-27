using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NewSIGASE.Models.Enum;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace SIGASE.Models
{
	public class Usuario
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(50)")]
		public string Matricula { get; set; }

		[Required]
		[Index("UN_Email", 0, IsUnique = true)]
		[Column(TypeName = "VARCHAR(255)")]
		public string Email { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(255)")]
		public string Nome { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(50)")]
		public string Senha { get; set; }

		[Required]
		public EnumTipoPerfil Perfil { get; set; }

		public Usuario(string matricula, 
			string email, 
			string nome,  
			EnumTipoPerfil perfil)
		{
			Id = Guid.NewGuid();
			Matricula = matricula;
			Email = email;
			Nome = nome;
			Senha = Guid.NewGuid().ToString();
			Perfil = perfil;
		}

		public Usuario()
		{ }
	}
}
