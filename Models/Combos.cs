using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Models
{
    public class Combos
    {
        static public List<SelectListItem> retornarOpcoesCadastro()
        {
            List<SelectListItem> cadastroOptions = new List<SelectListItem>()
            {
                new SelectListItem() { Text="Equipamentos", Value="Ep"},
                new SelectListItem() { Text="Salas", Value="Sa"}
            };
            return cadastroOptions;
        }

    }
}
