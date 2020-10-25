using System;
using System.Collections.Generic;

namespace NewSIGASE.Models
{
	public class Equipamento
	{
		public Guid Id { get; set; }
		public string Serial { get; set; }
		public string Nome { get; set; }
		public string Modelo { get; set; }

		public Sala Sala { get; set; }
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
