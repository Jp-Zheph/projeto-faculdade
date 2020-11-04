using Microsoft.AspNetCore.Mvc;
using NewSIGASE.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Dto.Request
{
    public class AgendamentoRelatorioFiltroDto
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Sala { get; set; }
        public string Usuario { get; set; }
        public EnumTipoPerfil PerfilUsuario { get; set; }
        public EnumTipoSala TipoLocal { get; set; }
    }
}
