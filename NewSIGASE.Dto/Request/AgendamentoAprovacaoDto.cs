using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Models.Enum;
using System;

namespace NewSIGASE.Dto.Request
{
    public class AgendamentoAprovacaoDto : Notifiable
    {
        public Guid AgendamentoId { get; set; }
        public EnumStatusAgendamento Status { get; set; }
        public string Justificativa { get; set; }

    }
}
