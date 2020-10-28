using NewSIGASE.Models.Enum;
using NewSIGASE.Models;
using System;

namespace NewSIGASE.Dto.Response
{
    public class UsuarioListaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; }
        public string Matricula { get; }
        public string Email { get; }
        public EnumTipoPerfil Perfil { get; }

        public UsuarioListaDto(Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            Matricula = usuario.Matricula;
            Email = usuario.Email;
            Perfil = usuario.Perfil;
        }
    }
}
