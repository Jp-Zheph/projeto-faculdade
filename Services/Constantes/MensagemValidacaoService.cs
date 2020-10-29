
namespace NewSIGASE.Services.Constantes
{
    public static class MensagemValidacaoService
    {
        internal static class Usuario
        {            
            internal static readonly string Inativo = "Usuário inativo.";
            internal static readonly string NaoExiste = "O usuário não existe.";
            internal static readonly string JaConfirmouCadastro = "O usuário já confirmou seu cadastro.";
            internal static readonly string SenhaDiferente = "Senha atual inválida.";
            internal static readonly string SenhaNaoPodeSerMatricula = "Senha não pode ser igual a matricula.";

            internal static string JaCadastrado(string campo) => string.Format("{0} já cadastrado.", campo);
        }
    }
}
