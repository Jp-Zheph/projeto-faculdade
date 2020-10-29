
using System;

namespace NewSIGASE.Infra.Configuration
{
    public class AppSettings
    {
        public EmailOptions Email { get; set; }
        public StringConexaoOptions StringConexao { get; set; }

        static string PerfilUsuario;
        static Guid UsuarioId;
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
