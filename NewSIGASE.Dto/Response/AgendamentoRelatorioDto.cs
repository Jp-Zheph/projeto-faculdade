
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NewSIGASE.Dto.Response
{
    public class AgendamentoRelatorioDto
    {
        public string DataCriacao{ get; set; }
        public string AgendadaPor { get; set; }
        public string MatriculaUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string PerfilUsuario { get; set; }
        public string DataAgendada { get; set; }
        public string Periodo { get; set; }
        public string Sala { get; set; }

        [DisplayFormat(DataFormatString = "{0:F3}")]
        public decimal Area { get; set; }
        public string TipoSala { get; set; }
        public int CapacidadeAlunos { get; set; }
        public int QuantidadeEquipamentos { get; set; }
        public string DataAprovacao { get; set; }
        public string AprovadaPor { get; set; }
        public string MatriculaAprovador { get; set; }
        public string EmailAprovador { get; set; }
        public string PerfilAprovador { get; set; }
        public string Status { get; set; }

        public AgendamentoRelatorioDto(Agendamento agendamento, Usuario aprovador)
        {
            DataCriacao = string.Format("{0:dd/MM/yyyy HH:mm}", agendamento.DataCriacao);
            AgendadaPor = agendamento.Usuario.Nome;
            MatriculaUsuario = agendamento.Usuario.Matricula;
            EmailUsuario = agendamento.Usuario.Email;
            PerfilUsuario = agendamento.Usuario.Perfil.ToString();
            DataAgendada = string.Format("{0:dd/MM/yyyy}", agendamento.DataAgendada);
            Periodo = agendamento.Periodo.ToString();
            Sala = agendamento.Sala.IdentificadorSala;
            Area = agendamento.Sala.Area;
            TipoSala = agendamento.Sala.Tipo == EnumTipoSala.Laboratorio ? agendamento.Sala.Tipo.ToString() : "Sala de Aula";
            CapacidadeAlunos = agendamento.Sala.CapacidadeAlunos;
			QuantidadeEquipamentos = agendamento.Sala.SalaEquipamentos?.Count() ?? 0;
            DataAprovacao = string.Format("{0:dd/MM/yyyy}", agendamento.DataAtualizacaoStatus);
            AprovadaPor = aprovador?.Nome ?? string.Empty;
            MatriculaAprovador = aprovador?.Matricula ?? string.Empty;
            EmailAprovador = aprovador?.Email ?? string.Empty;
            PerfilAprovador = aprovador?.Perfil.ToString() ?? string.Empty;
            Status = agendamento.Status.ToString();
        }
    }
}
