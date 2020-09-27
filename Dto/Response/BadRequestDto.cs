using Flunt.Notifications;
using System.Collections.Generic;

namespace NewSIGASE.Dto.Response
{
    public sealed class BadRequestDto
    {
        public IReadOnlyCollection<Notification> Erros { get; }

        public BadRequestDto(IReadOnlyCollection<Notification> erros)
        {
            Erros = erros;
        }
    }
}
