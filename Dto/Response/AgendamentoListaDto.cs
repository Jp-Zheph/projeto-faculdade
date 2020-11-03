﻿using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;

namespace NewSIGASE.Dto.Response
{
    public class AgendamentoListaDto
    {
        public Guid Id { get; set; }
        public string DataCriacao { get; set; }
        public string DataAgendada { get; set; }
        public string Periodo { get; set; }
        public string Status { get; set; }
        public string Sala { get; set; }
        public string Usuario { get; set; }
        public string TipoSala { get; set;  }

        public AgendamentoListaDto(Agendamento agendamento)
        {
            Id = agendamento.Id;
            DataCriacao = string.Format("{0:dd/MM/yyyy HH:mm}", agendamento.DataCriacao);
            DataAgendada = string.Format("{0:dd/MM/yyyy}", agendamento.DataAgendada);
            Periodo = agendamento.Periodo.ToString();
            Status = agendamento.Status.ToString();
            Sala = agendamento.Sala.IdentificadorSala;
            Usuario = agendamento.Usuario.Nome;
            TipoSala = agendamento.Sala.Tipo == EnumTipoSala.Laboratorio ? agendamento.Sala.Tipo.ToString() : "Sala de Aula";
        }
    }
}
