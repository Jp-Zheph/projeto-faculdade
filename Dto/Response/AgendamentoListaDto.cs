using NewSIGASE.Models;
using System;

namespace NewSIGASE.Dto.Response
{
    public class AgendamentoListaDto
    {
        public Guid Id { get; set; }
        public string DataCriacao { get; set; }
        public string DataAgendada { get; set; }
        public string Periodo { get; set; }
        public bool Status { get; set; }
        public string Sala { get; set; }
        public string Usuario { get; set; }

        public AgendamentoListaDto(Agendamento agendamento)
        {
            Id = agendamento.Id;
            DataCriacao = string.Format("{0:dd/MM/yyyy HH:mm}", agendamento.DataCriacao);
            DataAgendada = string.Format("{0:dd/MM/yyyy}", agendamento.DataAgendada);
            Periodo = agendamento.Periodo.ToString();
            Status = agendamento.Status;
            Sala = agendamento.Sala.IdentificadorSala;
            Usuario = agendamento.Usuario.Nome;
        }
    }
}
