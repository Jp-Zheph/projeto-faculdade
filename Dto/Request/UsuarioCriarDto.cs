using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Models.Enum;
using System;

namespace NewSIGASE.Dto.Request
{
    public class UsuarioCriarDto : Notifiable, IValidatable
    {
        public string Nome { get; }
        public string Matricula { get; }
        public string Email { get; }
        public EnumTipoPerfil Perfil { get; }

        public void Validate()
        {
            var perfilExiste = Enum.IsDefined(typeof(EnumTipoPerfil), Perfil);

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, nameof(Nome), MensagemValidacao.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Nome, 255, nameof(Nome), MensagemValidacao.CampoLimite255Caracteres)

                .IsNotNullOrEmpty(Matricula, nameof(Matricula), MensagemValidacao.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Matricula, 255, nameof(Matricula), MensagemValidacao.CampoLimite50Caracteres)

                .IsNotNullOrEmpty(Email, nameof(Email), MensagemValidacao.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Email, 255, nameof(Email), MensagemValidacao.CampoLimite255Caracteres)
                .IfNotNull(Email, x => x.IsEmail(Email, nameof(Email), MensagemValidacao.CampoFormatoIncorreto))

                .IsTrue(perfilExiste, nameof(Perfil), MensagemValidacao.CampoTipoInvalido(nameof(Perfil)))
            );

        }
    }
}
