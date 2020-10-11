using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Models.Enum;
using SIGASE.Models;
using System;

namespace NewSIGASE.Dto.Request
{
    public class UsuarioEditarDto : Notifiable, IValidatable
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string Email { get; set; }
        public EnumTipoPerfil Perfil { get; set; }
        public bool Ativo { get; set; }

        public UsuarioEditarDto(Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            Matricula = usuario.Matricula;
            Email = usuario.Email;
            Perfil = usuario.Perfil;
            Ativo = usuario.Ativo;
        }

        public UsuarioEditarDto()
        { }

        public void Validate()
        {
            var perfilExiste = Enum.IsDefined(typeof(EnumTipoPerfil), Perfil);

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, nameof(Nome), MensagemValidacaoDto.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Nome, 255, nameof(Nome), MensagemValidacaoDto.CampoLimite255Caracteres)

                .IsNotNullOrEmpty(Matricula, nameof(Matricula), MensagemValidacaoDto.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Matricula, 255, nameof(Matricula), MensagemValidacaoDto.CampoLimite50Caracteres)

                .IsNotNullOrEmpty(Email, nameof(Email), MensagemValidacaoDto.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Email, 255, nameof(Email), MensagemValidacaoDto.CampoLimite255Caracteres)
                .IfNotNull(Email, x => x.IsEmail(Email, nameof(Email), MensagemValidacaoDto.CampoFormatoIncorreto))

                .IsTrue(perfilExiste, nameof(Perfil), MensagemValidacaoDto.CampoTipoInvalido(nameof(Perfil)))
            );

        }
    }
}
