using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NewSIGASE.Models.Enum;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace NewSIGASE.Models
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

        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }


		[Index("UN_Documento", 0, IsUnique = true)]
		public string Documento { get; set; }

        public Endereco Endereco { get; set; }
        public Guid EnderecoId { get; set; }

        public List<Agendamento> Agendamentos { get; set; }

		public Usuario(string matricula, 
			string email, 
			string nome,  
			EnumTipoPerfil perfil,
			Endereco endereco,
			string telefone,
			DateTime dataNasc,
			string documento)
		{
			Id = Guid.NewGuid();
			Matricula = matricula;
			Email = email;
			Nome = nome;
			Senha = matricula;
			Perfil = perfil;
			Ativo = true;
			DataCriacao = DateTime.Now;
			Endereco = endereco;
			EnderecoId = endereco.Id;
			Telefone = telefone;
			DataNascimento = dataNasc;
			Documento = documento;
		}

		public Usuario()
		{ }

		public void AlterarSenha(string senha)
        {
			Senha = senha;
        }

		public void Editar(string matricula,
			string email,
			string nome,
			EnumTipoPerfil perfil,
			bool ativo,
			string telefone,
			DateTime dataNasc,
			string documento)
        {
			Matricula = matricula;
			Email = email;
			Nome = nome;
			Perfil = perfil;
			Ativo = ativo;
			Telefone = telefone;
			DataNascimento = dataNasc;
			Documento = documento;
		}

		public void AlterarSenha()
        {
			Senha = Matricula;
        }
	}
}
