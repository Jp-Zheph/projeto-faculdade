using NewSIGASE.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Models
{
    public class AgendamentoAprovacao
    {
        public Guid AgendamentoId { get; set; }
        public EnumStatusAgendamento Status { get; set; }
        public string Justificativa { get; set; }
    }
}
