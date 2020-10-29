using NewSIGASE.Models;
using System;

namespace NewSIGASE.Dto.Response
{
    public class AgendamentoListaDto
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAgendada { get; set; }
        public string Periodo { get; set; }
        public bool Status { get; set; }
        public string Sala { get; set; }
        public string Usuario { get; set; }

        public AgendamentoListaDto(Agendamento agendamento)
        {
            Id = agendamento.Id;
            DataCriacao = agendamento.DataCriacao;
            DataAgendada = agendamento.DataAgendada.Date;
            Periodo = agendamento.Periodo.ToString();
            Status = agendamento.Status;
            Sala = agendamento.Sala.IdentificadorSala;
            Usuario = agendamento.Usuario.Nome;
        }
    }
}
