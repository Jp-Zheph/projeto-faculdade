using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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

        static public List<SelectListItem> retornarOpcoesPerfil()
        {
            List<SelectListItem> cadastroOptions = new List<SelectListItem>()
            {
                new SelectListItem() { Text="Selecione", Value=""},
                new SelectListItem() { Text="Administrador", Value="0"},
                new SelectListItem() { Text="Professor", Value="1"}
            };
            return cadastroOptions;
        }

    }
}
