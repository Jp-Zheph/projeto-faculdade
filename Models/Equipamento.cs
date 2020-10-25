using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace NewSIGASE.Models
{
	public class Equipamento
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[Index("UN_Serial", 0, IsUnique = true)]
		[Column(TypeName = "VARCHAR(255)")]
		public string Serial { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(255)")]
		public string Nome { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(255)")]
		public string Modelo { get; set; }

		//public Sala Sala { get; set; }
		public Guid? SalaId { get; set; }

		public Equipamento(string serial, 
			string nome, 
			string modelo,
			Guid? salaId)
		{
			Id = Guid.NewGuid();
			Serial = serial;
			Nome = nome;
			Modelo = modelo;
			SalaId = salaId;
		}

		public Equipamento()
		{ }
	}
}
