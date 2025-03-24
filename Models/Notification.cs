namespace NotificationSystem.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Recipient { get; set; }//Email or Phone Number
        public string NotificationType {  get; set; }//Email or SMS
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;

    }
}
