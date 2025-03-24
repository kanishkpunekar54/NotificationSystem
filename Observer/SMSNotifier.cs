using NotificationSystem.Models;

namespace NotificationSystem.Observer
{
    public class SMSNotifier : INotificationObserver
    {
        public void Update(Notification notification)
        {
            if(notification.NotificationType == "SMS")
            {
                Console.WriteLine($"SMS sent to {notification.Recipient} : {notification.Message}");
            }
        }
    }
}
