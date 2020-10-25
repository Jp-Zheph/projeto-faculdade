using NewSIGASE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Dto.Response
{
    public class EquipamentoListaDto
    {
        public Guid Id { get; set; }
        public string Serial { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }

        public EquipamentoListaDto(Equipamento equipamento)
        {
            Id = equipamento.Id;
            Serial = equipamento.Serial;
            Nome = equipamento.Nome;
            Modelo = equipamento.Modelo;
        }
    }
}
