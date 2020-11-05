using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NewSIGASE.Models
{
    public class Combos
    {
        static public List<SelectListItem> retornarOpcoesPerfil()
        {
            List<SelectListItem> cadastroOptions = new List<SelectListItem>()
            {
                new SelectListItem() { Text="Selecione", Value="0"},
                new SelectListItem() { Text="Administrador", Value="1"},
                new SelectListItem() { Text="Professor", Value="2"}
            };
            return cadastroOptions;
        }

        static public List<SelectListItem> retornarOpcoesStatusUsuario()
        {
            List<SelectListItem> cadastroOptions = new List<SelectListItem>()
            {
                new SelectListItem() { Text="Ativo", Value="True"},
                new SelectListItem() { Text="Inativo", Value="False"}
            };
            return cadastroOptions;
        }

        static public List<SelectListItem> retornarOpcoesSala()
        {
            List<SelectListItem> cadastroOptions = new List<SelectListItem>()
            {
                new SelectListItem() { Text="Selecione", Value="0"},
                new SelectListItem() { Text="Sala de Aula", Value="1"},
                new SelectListItem() { Text="Laboratório", Value="2"}
            };
            return cadastroOptions;
        }

        static public List<SelectListItem> retornarOpcoesPeriodo()
        {
            List<SelectListItem> cadastroOptions = new List<SelectListItem>()
            {
                new SelectListItem() { Text="Selecione", Value=""},
                new SelectListItem() { Text="Matutino", Value="0"},
                new SelectListItem() { Text="Vespertino", Value="1"},
                new SelectListItem() { Text="Noturno", Value="2"}
            };
            return cadastroOptions;
        }
    }

}
