
namespace NewSIGASE.Dto
{
    public class MensagemValidacao
    {
        public const string CampoObrigatorio = "O campo é obrigatório.";
        public const string CampoFormatoIncorreto = "O campo está em um formato incorreto.";
        public const string CampoAceitaNumeros = "O campo aceita apenas números.";
        public const string CampoAceitaStrings = "O campo aceita apenas letras.";
        public const string CampoNaoLetras= "O campo nao aceita  letras.";
        public const string CampoLimite50Caracteres = "O campo aceita até 50 caracteres.";
        public const string CampoLimite255Caracteres = "O campo aceita até 255 caracteres.";
        public const string CampoSenhaMinimoCaracteres = "O campo deve ter no mínimo 6 caracteres";
        public const string CampoNovaSenhaIgualSenhaAtual = "A nova senha não pode ser igual a senha atual.";

        public static string CampoTipoInvalido(string tipo) => string.Format("O tipo de {0} é inválido.", tipo);
    }
}
