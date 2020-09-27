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

		public List<SalaEquipamento> SalasEquipamentos { get; set; }

		public Equipamento(string serial, 
			string nome, 
			string modelo)
		{
			Id = Guid.NewGuid();
			Serial = serial;
			Nome = nome;
			Modelo = modelo;
			SalasEquipamentos = new List<SalaEquipamento>();
		}

		public Equipamento()
		{ }
	}
}
