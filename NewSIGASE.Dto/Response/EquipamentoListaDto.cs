using NewSIGASE.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Response
{
    public class EquipamentoListaDto
    {
        public Guid Id { get; set; }
        public string Serial { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Cor { get; set; }

        [DisplayFormat(DataFormatString = "{0:F3}")]
        public decimal? Peso { get; set; }

        [DisplayFormat(DataFormatString = "{0:F3}")]
        public decimal? Comprimento { get; set; }

        [DisplayFormat(DataFormatString = "{0:F3}")]
        public decimal? Largura { get; set; }

        [DisplayFormat(DataFormatString = "{0:F3}")]
        public decimal? Altura { get; set; }

		public EquipamentoListaDto(Equipamento equipamento)
        { 
			Id = equipamento.Id;
			Serial = equipamento.Serial;
			Nome = equipamento.Nome;
			Modelo = equipamento.Modelo;
            Marca = equipamento.Marca;
			Cor = equipamento.Cor;
			Peso = equipamento.Peso;
			Comprimento = equipamento.Comprimento;
			Largura = equipamento.Largura;
			Altura = equipamento.Altura;
		}
	
    }
}
