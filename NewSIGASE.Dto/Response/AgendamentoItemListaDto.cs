using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;

namespace NewSIGASE.Dto.Response
{
    public class AgendamentoItemListaDto
    {
        public Guid Id { get; set; }
        public string DataCriacao { get; set; }
        public string DataAgendada { get; set; }
        public string Periodo { get; set; }
        public string Status { get; set; }
        public string Sala { get; set; }
        public string StatusSala { get; set; }
        public string Observacoes { get; set; }
        public string Usuario { get; set; }
        public string TipoSala { get; set;  }

        public AgendamentoItemListaDto(Agendamento agendamento)
        {
            var nomeUsuario = agendamento.Usuario.Nome.Split(" ");

            Id = agendamento.Id;
            DataCriacao = string.Format("{0:dd/MM/yyyy HH:mm}", agendamento.DataCriacao);
            DataAgendada = string.Format("{0:dd/MM/yyyy}", agendamento.DataAgendada);
            Periodo = agendamento.Periodo.ToString();
            Status = agendamento.Status.ToString();
            Sala = agendamento.Sala.IdentificadorSala;
            Usuario = nomeUsuario.Length > 1 ? $"{nomeUsuario[0]} {nomeUsuario[1]}" : $"{nomeUsuario[0]}";
            TipoSala = agendamento.Sala.Tipo == EnumTipoSala.Laboratorio ? agendamento.Sala.Tipo.ToString() : "Sala de Aula";
            StatusSala = agendamento.Sala.Ativo ? "Ativo" : "Inativo";
            Observacoes = agendamento.Sala.Observacao ?? string.Empty;
        }
    }
}
