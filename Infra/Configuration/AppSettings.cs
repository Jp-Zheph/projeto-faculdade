
namespace NewSIGASE.Infra.Configuration
{
    public class AppSettings
    {
        public EmailOptions Email { get; set; }
        public StringConexaoOptions StringConexao { get; set; }
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
