﻿
namespace NewSIGASE.Dto
{
    public class MensagemValidacao
    {
        public const string CampoObrigatorio = "O campo é obrigatório.";
        public const string CampoFormatoIncorreto = "O campo está em um formato incorreto.";
        public const string CampoLimite50Caracteres = "O campo aceita até 50 caracteres.";
        public const string CampoLimite255Caracteres = "O campo aceita até 255 caracteres.";

        public static string CampoTipoInvalido(string tipo) => string.Format("O tipo de {0} é inválido.", tipo);
    }
}
