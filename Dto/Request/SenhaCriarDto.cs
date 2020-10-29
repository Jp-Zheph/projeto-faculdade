
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Request
{
    public sealed class SenhaCriarDto : Notifiable, IValidatable
    {
        [Required]
        public Guid Id{ get; set; }

        [Required]
        public string SenhaAtual { get; set; }

        [Required]
        public string SenhaNova { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(SenhaAtual, nameof(SenhaAtual), MensagemValidacaoDto.CampoObrigatorio)

                .IsNotNullOrEmpty(SenhaNova, nameof(SenhaNova), MensagemValidacaoDto.CampoObrigatorio)
                .IfNotNull(SenhaNova, x => x.HasMinLen(SenhaNova, 6, nameof(SenhaNova), MensagemValidacaoDto.CampoSenhaMinimoCaracteres))

                .IfNotNull(SenhaNova, x => x.AreNotEquals(SenhaNova, SenhaAtual, nameof(SenhaNova), MensagemValidacaoDto.CampoNovaSenhaIgualSenhaAtual, System.StringComparison.OrdinalIgnoreCase))
            );
        }
    }
}
