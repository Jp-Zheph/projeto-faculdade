using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Request
{
    public class EquipamentoDto : Notifiable, IValidatable
    {
        public Guid? Id { get; set; }

        [Required]
        public string Serial { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Modelo { get; set; }

        public DateTime DataCriacao { get; set; }
        public decimal Peso { get; set; }
        public string Cor { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }


        public EquipamentoDto(Equipamento equipamento)
        {
            Id = equipamento.Id;
            Serial = equipamento.Serial;
            Nome = equipamento.Nome;
            Modelo = equipamento.Modelo;
            Cor = equipamento.Cor;
            Peso = equipamento.Peso;
            Comprimento = equipamento.Comprimento;
            Largura = equipamento.Largura;
            Altura = equipamento.Altura;
        }

        public EquipamentoDto()
        { }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, nameof(Nome), MensagemValidacaoDto.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Nome, 255, nameof(Nome), MensagemValidacaoDto.CampoLimite255Caracteres)

                .IsNotNullOrEmpty(Serial, nameof(Serial), MensagemValidacaoDto.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Serial, 255, nameof(Serial), MensagemValidacaoDto.CampoLimite50Caracteres)

                .IsNotNullOrEmpty(Modelo, nameof(Modelo), MensagemValidacaoDto.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Modelo, 255, nameof(Modelo), MensagemValidacaoDto.CampoLimite255Caracteres)
            );
        }
    }
}
