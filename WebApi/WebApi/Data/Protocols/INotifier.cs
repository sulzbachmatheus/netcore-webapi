using System.Collections.Generic;
using WebApi.Services;

namespace WebApi.Data.Protocols
{
    public interface INotifier
    {
        void Handle(Notification notification);
        List<Notification> GetNotifications();
        bool HasNotification();
        
    }
}
