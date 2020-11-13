using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Request
{
    public class SalaDto : Notifiable, IValidatable
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public EnumTipoSala Tipo { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public string IdentificadorSala { get; set; }  // numero da sala

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public int CapacidadeAlunos { get; set; }

        [RegularExpression("^[0-9,.]+$",ErrorMessage = MensagemValidacao.CampoNaoLetras)]
        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        [DisplayFormat(DataFormatString = "{0:F3}")]
        public string Area { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public int Andar { get; set; }
        public string Observacao { get; set; }
        public bool Ativo { get; set; }
        public List<Guid> EquipamentoId { get; set; }

        public SalaDto(Sala sala)
        {
            Tipo = sala.Tipo;
            IdentificadorSala = sala.IdentificadorSala;
            CapacidadeAlunos = sala.CapacidadeAlunos;
            Area = sala.Area.ToString();
            Andar = sala.Andar;
            Ativo = sala.Ativo;
            Observacao = sala.Observacao;
        }

        public SalaDto()
        { }

        public void Validate()
        {
            var tipoSalaExiste = Enum.IsDefined(typeof(EnumTipoSala), Tipo);

            AddNotifications(new Contract()
                .IsTrue(tipoSalaExiste, nameof(Tipo), MensagemValidacao.CampoTipoInvalido(nameof(Tipo)))

                .IsNotNullOrEmpty(IdentificadorSala, nameof(IdentificadorSala), MensagemValidacao.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(IdentificadorSala, 100, nameof(IdentificadorSala), MensagemValidacao.CampoLimite50Caracteres)

                .IsTrue(CapacidadeAlunos > 0, nameof(CapacidadeAlunos), MensagemValidacao.CampoTipoInvalido(nameof(CapacidadeAlunos)))

                .IsNotNullOrEmpty(Area, nameof(Area), MensagemValidacao.CampoObrigatorio)
            );
        }
    }
}
