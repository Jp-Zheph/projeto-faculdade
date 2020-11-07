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
        public EnumStatusAgendamento Status { get; set; }

        [Required]
        public Guid SalaId { get; set; }

        public SalaDto Sala { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        public UsuarioDto Usuario { get; set; }
        public string Justificativa { get; set; }

        public void Validate()
        {
            var periodoExiste = Enum.IsDefined(typeof(EnumPeriodo), Periodo);

            AddNotifications(new Contract()
                .IsTrue(DataAgendada.Date > DateTime.Now.Date, nameof(DataAgendada), "A data do agendamento deve ser maior que a data atual.")
                .IsTrue(periodoExiste, nameof(Periodo), MensagemValidacao.CampoTipoInvalido(nameof(Periodo)))
            );
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
            Justificativa = agendamento.Justificativa;
        }

        public AgendamentoDto()
        { }
    }
}
