﻿using Flunt.Notifications;
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

        public UsuarioDto(Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            Matricula = usuario.Matricula;
            Email = usuario.Email;
            Perfil = usuario.Perfil;
            Ativo = usuario.Ativo;
        }

        public UsuarioDto()
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