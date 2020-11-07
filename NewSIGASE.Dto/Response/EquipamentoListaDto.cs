using NewSIGASE.Models;
using System;

namespace NewSIGASE.Dto.Response
{
    public class EquipamentoListaDto
    {
        public Guid Id { get; set; }
        public string Serial { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Comprimento { get; set; }
        public decimal? Largura { get; set; }
        public decimal? Altura { get; set; }

		public EquipamentoListaDto(Equipamento equipamento)
        { 
			Id = equipamento.Id;
			Serial = equipamento.Serial;
			Nome = equipamento.Nome;
			Modelo = equipamento.Modelo;
			Cor = equipamento.Cor;
			Peso = equipamento.Peso;
			Comprimento = equipamento.Comprimento;
			Largura = equipamento.Largura;
			Altura = equipamento.Altura;
		}
	
    }
}
