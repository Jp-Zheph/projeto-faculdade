using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NewSIGASE.Comum
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

        static public List<SelectListItem> retornarOpcoesStatus()
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
                new SelectListItem() { Text="Matutino   - (08:00-12:00)", Value="0"},
                new SelectListItem() { Text="Vespertino - (13:00-17:00)", Value="1"},
                new SelectListItem() { Text="Noturno    - (19:00-22:00)", Value="2"}
            };
            return cadastroOptions;
        }

        static public List<SelectListItem> retornarOpcoesStatusAgendamento()
        {
            List<SelectListItem> cadastroOptions = new List<SelectListItem>()
            {
                new SelectListItem() { Text="Selecione", Value=""},
                new SelectListItem() { Text="Pendente", Value="0"},
                new SelectListItem() { Text="Aprovado", Value="1"},
                new SelectListItem() { Text="Reprovado", Value="2"},
                new SelectListItem() { Text="Cancelado", Value="3"}
            };
            return cadastroOptions;
        }
    }

}
