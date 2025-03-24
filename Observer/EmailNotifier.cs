using NotificationSystem.Models;

namespace NotificationSystem.Observer
{
    public class EmailNotifier : INotificationObserver
    {
        public void Update(Notification notification)
        {
            if(notification.NotificationType == "Email")
            {
                Console.WriteLine($"Email sent to {notification.Recipient} : {notification.Message}");
            }
        }
    }
}
