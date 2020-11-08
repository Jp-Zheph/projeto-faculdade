
namespace NewSIGASE.Dto.Response.IntegracaoCorreios
{
    public class ConsultaCepDto
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Complemento { get; set; }

        public ConsultaCepDto(string logradouro, 
            string bairro, 
            string cep, 
            string cidade, 
            string uF, 
            string complemento)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            UF = uF;
            Complemento = complemento;
        }
    }
}
