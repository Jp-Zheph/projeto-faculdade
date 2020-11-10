
using NewSIGASE.Models.Enum;
using System;
using System.Collections.Generic;

namespace NewSIGASE.Infra.Configuration
{
    public class AppSettings
    {
        public EmailOptions Email { get; set; }
        public StringConexaoOptions StringConexao { get; set; }

        static string PerfilUsuario;
        static Guid UsuarioId;
        static string NomeUsuario;
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
        public static Guid Usuario
        {
            get
            {
                return UsuarioId;
            }
            set
            {
                UsuarioId = value;
            }
        }

        public static string Nome
        {
            get
            {
                return NomeUsuario;
            }
            set
            {
                NomeUsuario = value;
            }
        }

        public static List<String> PerfisAutorizadosMenu
        {
            get
            {
                List<String> lista = new List<string>();
                lista.Add(EnumTipoPerfil.Diretor.ToString());
                lista.Add(EnumTipoPerfil.Administrador.ToString());
                return lista;
            }
        }
    }

    public class EmailOptions
    {
        public const string Email = "Email";

        public string ApiKey { get; set; }
        public string NomeRemetente { get; set; }
        public string EmailRemetente { get; set; }
        public string UrlLogin { get; set; }
    }

    public class StringConexaoOptions
    {
        public const string StringConexao = "StringConexao";

        public string SIGASEContext { get; set; }
    }


}
