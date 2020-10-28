using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Models
{
    public static class Global
    {
        static string PerfilUsuario;
        public static string Perfil
        {
            get
            {
                return PerfilUsuario;
            }
            set
            {
                PerfilUsuario = value;
            }
        }
    }
}
