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

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string Marca { get; set; }

        public DateTime DataCriacao { get; set; }

        [Column(TypeName = "DECIMAL(18,3)")]
        public decimal? Peso { get; set; }
        public string Cor { get; set; }

        [Column(TypeName = "DECIMAL(18,3)")]
        public decimal? Comprimento { get; set; }

        [Column(TypeName = "DECIMAL(18,3)")]
        public decimal? Largura { get; set; }

        [Column(TypeName = "DECIMAL(18,3)")]
        public decimal? Altura { get; set; }

		public SalaEquipamento SalaEquipamento { get; set; }

		public Equipamento(string serial,
            string nome,
            string modelo,
            decimal? peso,
            string cor,
            decimal? comprimento,
            decimal? largura,
            decimal? altura,
            string marca)
        {
            Serial = serial;
            Nome = nome;
            Modelo = modelo;
            DataCriacao = DateTime.Now;
            Peso = peso;
            Cor = cor;
            Comprimento = comprimento;
            Largura = largura;
            Altura = altura;
            Marca = marca;
        }

        public Equipamento()
        { }

        public string NomeModelo
        {
            get
            {
                return $"{this.Nome} {this.Marca} - {this.Modelo}";
            }
        }

        public void Editar(string serial,
            string nome,
            string modelo,
            decimal? peso,
            string cor,
            decimal? comprimento,
            decimal? largura,
            decimal? altura,
            string marca)
        {
            Serial = serial;
            Nome = nome;
            Modelo = modelo;
            Peso = peso;
            Cor = cor;
            Comprimento = comprimento;
            Largura = largura;
            Altura = altura;
            Marca = marca;
        }

    }
}
