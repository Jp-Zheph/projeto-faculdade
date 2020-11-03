using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewSIGASE.Models
{
    public class Endereco
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Logradouro { get; set; }

        [Column(TypeName = "VARCHAR(20)")]
        public string Numero { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Complemento { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Bairro { get; set; }

        [Column(TypeName = "VARCHAR(8)")]
        public string Cep { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Cidade { get; set; }

        [Column(TypeName = "VARCHAR(2)")]
        public string UF { get; set; }

        [Column(TypeName = "VARCHAR(2)")]
        public string Pais { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string PontoReferencia { get; set; }

        protected Endereco(
            string logradouro,
            string numero,
            string bairro,
            string cep,
            string cidade,
            string uf,
            string pais,
            string complemento,
            string pontoReferencia)
        {
            Id = Guid.NewGuid();
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            UF = uf;
            Pais = pais;
            DataCriacao = DateTime.UtcNow;
            PontoReferencia = pontoReferencia;
        }

        protected Endereco()
        { }
    }
}
