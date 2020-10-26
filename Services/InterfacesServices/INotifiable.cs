using Flunt.Notifications;
using System.Collections.Generic;

namespace NewSIGASE.Services.InterfacesServices
{
    public interface INotifiable
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool Invalid { get; }
        bool Valid { get; }
    }
}