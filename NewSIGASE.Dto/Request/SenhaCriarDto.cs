
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Request
{
    public sealed class SenhaCriarDto : Notifiable, IValidatable
    {
        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public string SenhaNova { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(SenhaAtual, nameof(SenhaAtual), MensagemValidacao.CampoObrigatorio)

                .IsNotNullOrEmpty(SenhaNova, nameof(SenhaNova), MensagemValidacao.CampoObrigatorio)
                .IfNotNull(SenhaNova, x => x.HasMinLen(SenhaNova, 6, nameof(SenhaNova), MensagemValidacao.CampoSenhaMinimoCaracteres))

                .IfNotNull(SenhaNova, x => x.AreNotEquals(SenhaNova, SenhaAtual, nameof(SenhaNova), MensagemValidacao.CampoNovaSenhaIgualSenhaAtual, System.StringComparison.OrdinalIgnoreCase))
            );
        }
    }
}
