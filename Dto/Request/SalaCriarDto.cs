using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Request
{
    public class SalaCriarDto : Notifiable, IValidatable
    {
        [Required]
        public EnumTipoSala TipoSala { get; set; }
        [Required]
        public string IdentificadorSala { get; set; }  // numero da sala
        [Required]
        public int CapacidadeAlunos { get; set; }
        [Required]
        public List<Equipamento> Equipamentos { get; set; }
       
        public void Validate()
        {
            var perfilExiste = Enum.IsDefined(typeof(EnumTipoSala), TipoSala);

            AddNotifications(new Contract()

               .IsTrue(perfilExiste, nameof(TipoSala), MensagemValidacaoDto.CampoTipoInvalido(nameof(TipoSala)))

                .IsNotNullOrEmpty(IdentificadorSala, nameof(IdentificadorSala), MensagemValidacaoDto.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(IdentificadorSala, 100, nameof(IdentificadorSala), MensagemValidacaoDto.CampoLimite50Caracteres)

                 .IsTrue(perfilExiste, nameof(CapacidadeAlunos), MensagemValidacaoDto.CampoTipoInvalido(nameof(CapacidadeAlunos)))

                 .IsTrue(perfilExiste, nameof(Equipamentos), MensagemValidacaoDto.CampoTipoInvalido(nameof(Equipamentos)))

            );

        }
    }
}
