using NotificationSystem.Models;

namespace NotificationSystem.Observer
{
    public interface INotificationObserver
    {
        void Update(Notification notification);
    }
}
