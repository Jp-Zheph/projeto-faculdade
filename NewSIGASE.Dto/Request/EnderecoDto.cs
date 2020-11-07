using NewSIGASE.Models;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Dto.Request
{
    public class EnderecoDto
    {
        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cep { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string UF { get; set; }

        public string Pais { get; set; }

        public string PontoReferencia { get; set; }

        public EnderecoDto(Endereco endereco)
        {
            Logradouro = endereco.Logradouro;
            Numero = endereco.Numero;
            Complemento = endereco.Complemento;
            Bairro = endereco.Bairro;
            Cep = endereco.Cep;
            Cidade = endereco.Cidade;
            UF = endereco.UF;
            Pais = endereco.Pais;
            PontoReferencia = endereco.PontoReferencia;
        }

        public EnderecoDto()
        { }
    }
}
