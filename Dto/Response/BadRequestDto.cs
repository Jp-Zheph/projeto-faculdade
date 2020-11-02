using Flunt.Notifications;
using System.Collections.Generic;

namespace NewSIGASE.Dto.Response
{
    public sealed class BadRequestDto
    {
        public IEnumerable<Notification> Mensagens { get; }
        public string Tipo { get; }

        public BadRequestDto(IEnumerable<Notification> mensagens, string tipo)
        {
            Tipo = tipo;
            Mensagens = mensagens;
        }
    }
}
