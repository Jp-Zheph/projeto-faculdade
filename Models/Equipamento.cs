﻿using System;
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

		public Sala Sala { get; set; }
		public Guid? SalaId { get; set; }

        public DateTime DataCriacao { get; set; }
        public decimal Peso { get; set; }
        public string Cor { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }

		public Equipamento(
			 string serial,
			string nome,
			string modelo,
			Guid? salaId
			, DateTime dataCriacao
			, decimal peso
			, string cor
			, decimal comprimento
			, decimal largura
			, decimal altura)
		{
			
			Serial = serial;
			Nome = nome;
			Modelo = modelo;
			
			SalaId = salaId;
			DataCriacao = DateTime.Now;
			Peso = peso;
			Cor = cor;
			Comprimento = comprimento;
			Largura = largura;
			Altura = altura;
		}


		/*
				public Equipamento(string serial, 
					string nome, 
					string modelo,
					Guid? salaId,
					decimal Peso,
					string Cor,
					decimal Comprimento,
					decimal Largura,
					decimal Altura)

				{
					Id = Guid.NewGuid();
					Serial = serial;
					Nome = nome;
					Modelo = modelo;
					SalaId = salaId;
					DataCriacao = DateTime.Now;
				}*/

		public string NomeModelo
        {
            get
            {
				return this.Nome + "-" + this.Modelo;
            }
        }

		public Equipamento()
		{ }

		public Equipamento(string serial, string nome, string modelo, object p)
		{
			Serial = serial;
			Nome = nome;
			Modelo = modelo;
		}

		public Equipamento(string serial, string nome, string modelo, object p, decimal comprimento, decimal altura, decimal largura, object p1) : this(serial, nome, modelo, p)
		{
		}
	}
}
