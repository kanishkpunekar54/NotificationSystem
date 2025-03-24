using NotificationSystem.Models;

namespace NotificationSystem.Observer
{
    public class NotificationManager
    {
        private readonly List<INotificationObserver> _observers = new();

        public void Subscribe(INotificationObserver observer)
        {
            _observers.Add(observer);
        }
        public void Unsubscribe(INotificationObserver observer)
        {
            _observers.Remove(observer);
        }
        public void NotifyObservers(Notification notification)
        {
            foreach(var observer in _observers)
            {
                observer.Update(notification);
            }
        }
    }
}
