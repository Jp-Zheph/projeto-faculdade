
using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Request
{
    public sealed class SenhaCriarDto : Notifiable, IValidatable
    {
        [Required]
        public string Senha { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Senha, nameof(Senha), MensagemValidacaoDto.CampoObrigatorio)
                .IfNotNull(Senha, x => x.HasMinLen(Senha, 6, nameof(Senha), MensagemValidacaoDto.CampoSenhaMinimoCaracteres))
            );
        }
    }
}
