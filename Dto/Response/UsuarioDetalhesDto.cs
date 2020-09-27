
using NewSIGASE.Models.Enum;
using SIGASE.Models;

namespace NewSIGASE.Dto.Response
{
    public class UsuarioDetalhesDto
    {
        public string Nome { get; set; }
        public string Matricula { get; }
        public string Email { get; set; }
        public EnumTipoPerfil Perfil { get; set; }

        public UsuarioDetalhesDto(Usuario usuario)
        {
            Nome = usuario.Nome;
            Matricula = usuario.Matricula;
            Email = usuario.Email;
            Perfil = usuario.Perfil;
        }
    }
}
