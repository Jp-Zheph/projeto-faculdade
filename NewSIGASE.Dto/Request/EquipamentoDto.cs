using Flunt.Notifications;
using Flunt.Validations;
using NewSIGASE.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Request
{
    public class EquipamentoDto : Notifiable, IValidatable
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public string Serial { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public string Nome { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public string Modelo { get; set; }

        [Required(ErrorMessage = MensagemValidacao.CampoObrigatorio)]
        public string Marca { get; set; }

        public DateTime DataCriacao { get; set; }

        [DisplayFormat(DataFormatString = "{0:F3}")]
        [RegularExpression("^[0-9]*$", ErrorMessage = MensagemValidacao.CampoFormatoIncorreto)]
        public string Peso { get ; set; }

        public string Cor { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:F3}")]
        [RegularExpression("^[0-9]*$", ErrorMessage = MensagemValidacao.CampoFormatoIncorreto)]
        public string Comprimento { get ; set ; }


        [DisplayFormat(DataFormatString = "{0:F3}")]
        [RegularExpression("^[0-9]*$", ErrorMessage = MensagemValidacao.CampoFormatoIncorreto)]
        public string Largura { get ; set ; }


        [DisplayFormat(DataFormatString = "{0:F3}")]
        [RegularExpression("^[0-9]*$", ErrorMessage = MensagemValidacao.CampoFormatoIncorreto)]
        public string Altura { get ; set ; }

        public EquipamentoDto(Equipamento equipamento)
		{
			Id = equipamento.Id;
			Serial = equipamento.Serial;
			Nome = equipamento.Nome;
			Modelo = equipamento.Modelo;
			Marca = equipamento.Marca;
			DataCriacao = equipamento.DataCriacao;
			Peso = equipamento.Peso.ToString();
			Cor = equipamento.Cor;
			Comprimento = equipamento.Comprimento.ToString();
			Largura = equipamento.Largura.ToString();
			Altura = equipamento.Altura.ToString();
		}

        public EquipamentoDto()
        { }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, nameof(Nome), MensagemValidacao.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Nome, 255, nameof(Nome), MensagemValidacao.CampoLimite255Caracteres)

                .IsNotNullOrEmpty(Serial, nameof(Serial), MensagemValidacao.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Serial, 255, nameof(Serial), MensagemValidacao.CampoLimite50Caracteres)

                .IsNotNullOrEmpty(Modelo, nameof(Modelo), MensagemValidacao.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Modelo, 255, nameof(Modelo), MensagemValidacao.CampoLimite255Caracteres)

                .IsNotNullOrEmpty(Marca, nameof(Marca), MensagemValidacao.CampoObrigatorio)
                .HasMaxLengthIfNotNullOrEmpty(Marca, 255, nameof(Marca), MensagemValidacao.CampoLimite255Caracteres)
            );
        }
    }
}
