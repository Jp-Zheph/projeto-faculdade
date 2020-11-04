using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Models.Enum;
using NewSIGASE.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Request
{
    public class UsuarioDto : Notifiable, IValidatable
    {
        public Guid? Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Matricula { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public EnumTipoPerfil Perfil { get; set; }

        [Required]
        public bool Ativo { get; set; }
        public Endereco Endereco { get; set; }
		public Usuario Usuario { get; }

		public UsuarioDto(Guid? id, string nome, string matricula, string email, EnumTipoPerfil perfil, bool ativo, Endereco endereco)
		{
			Id = id;
			Nome = nome;
			Matricula = matricula;
			Email = email;
			Perfil = perfil;
			Ativo = ativo;
			Endereco = endereco;
		}


		/*
				public UsuarioDto(Usuario usuario)
				{
					Id = usuario.Id;
					Nome = usuario.Nome;
					Matricula = usuario.Matricula;
					Email = usuario.Email;
					Perfil = usuario.Perfil;
					Ativo = usuario.Ativo;
					Endereco = usuario.Endereco;
				}*/


		public UsuarioDto()
        { }

		public UsuarioDto(Usuario usuario)
		{
			Usuario = usuario;
		}

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
