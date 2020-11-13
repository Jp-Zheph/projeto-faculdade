using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Models.Enum;
using NewSIGASE.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Flunt.Br.Extensions;
using System.Text.RegularExpressions;

namespace NewSIGASE.Dto.Request
{
    public class UsuarioDto : Notifiable, IValidatable
    {
        public Guid? Id { get; set; }

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = MensagemValidacao.CampoAceitaStrings)]
        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public string Nome { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public string Matricula { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public string Documento { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        [Phone(ErrorMessage = MensagemValidacao.CampoFormatoIncorreto)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        [EmailAddress(ErrorMessage = MensagemValidacao.CampoFormatoIncorreto)]
        public string Email { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public EnumTipoPerfil Perfil { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public EnderecoDto Endereco { get; set; }

        public UsuarioDto(Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            Matricula = usuario.Matricula;
            Email = usuario.Email;
            Perfil = usuario.Perfil;
            Ativo = usuario.Ativo;
            Documento = usuario.Documento;
            Telefone = usuario.Telefone;
            DataNascimento = usuario.DataNascimento;
            Endereco = new EnderecoDto(usuario.Endereco);
        }

        public UsuarioDto()
        { }

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

                .IsNotNullOrEmpty(Documento, nameof(Documento), MensagemValidacao.CampoObrigatorio)
                .IfNotNull(Documento, x => x.IsCpf(Documento, "CPF", MensagemValidacao.CampoFormatoIncorreto))

                .IsNotNullOrEmpty(Telefone, nameof(Telefone), MensagemValidacao.CampoObrigatorio)
                .IfNotNull(Telefone, x => x.IsTrue(Regex.IsMatch(Telefone, "^[0-9]*$"), nameof(Telefone), MensagemValidacao.CampoFormatoIncorreto))
            );
        }
    }
}
