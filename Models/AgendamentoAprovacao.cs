using NewSIGASE.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Models
{
    public class AgendamentoAprovacao
    {
        public EnumStatusAgendamento status { get; set; }
        public string justificativa { get; set; }
    }
}
