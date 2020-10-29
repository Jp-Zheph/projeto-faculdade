using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Request
{
    public class AgendamentoDto : Notifiable, IValidatable
    {
        public Guid? Id { get; set; }

        [Required]
        public DateTime DataAgendada { get; set; }

        [Required]
        public EnumPeriodo Periodo { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public Guid SalaId { get; set; }

        public SalaDto Sala { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        public UsuarioDto Usuario { get; set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }

        public AgendamentoDto(Agendamento agendamento)
        {
            Id = agendamento.Id;
            DataAgendada = agendamento.DataAgendada;
            Periodo = agendamento.Periodo;
            Status = agendamento.Status;
            SalaId = agendamento.SalaId;
            Sala = new SalaDto(agendamento.Sala);
            UsuarioId = agendamento.UsuarioId;
            Usuario = new UsuarioDto(agendamento.Usuario);
        }

        public AgendamentoDto()
        { }
    }
}
