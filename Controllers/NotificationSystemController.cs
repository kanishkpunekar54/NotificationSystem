using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationSystem.Data;
using NotificationSystem.Models;
using NotificationSystem.Observer;
using System.Reflection.Metadata.Ecma335;
namespace NotificationSystem.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("/api/notifications")]
    [ApiController]
    public class NotificationSystemController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<NotificationSystemController> _logger;
        private readonly NotificationManager _notificationManager;

        

        public NotificationSystemController(AppDbContext context,NotificationManager notificationManager,ILogger<NotificationSystemController> logger)
        {
            _context = context;
            _logger = logger;
            _notificationManager = notificationManager;
        }

        [HttpPost("subscribe")]
        public IActionResult Subscribe(string recipient,string notificationType)
        {
            if(notificationType == "Email")
            {
                _notificationManager.Subscribe(new EmailNotifier());
            }
            else if (notificationType == "SMS")
            {
                _notificationManager.Subscribe(new SMSNotifier());
            }
            else
            {
                return BadRequest("Invalid Notification type.Use Email or SMS");
            }
            return Ok($"{recipient} subscribed for {notificationType} notifications.");
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            _notificationManager.NotifyObservers(notification);
            _logger.LogInformation($"Notification sent : {notification.Message}");

            return Ok($"Notification Sent to {notification.Recipient} via {notification.NotificationType}");
        }


        [HttpGet("all/{recipient}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetAllNotifications(string recipient)
        {
            _logger.LogInformation("Fetching all notifications");
            var notifications = await _context.Notifications.Where(x => x.Recipient == recipient).ToListAsync();
            _logger.LogInformation($"Total number of notifications fetched are :{notifications.Count}");
            return await _context.Notifications.ToListAsync();
        }

        [HttpPut("{id}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            _logger.LogInformation("Received request to mark notification {Id} as read ",id);
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                _logger.LogWarning("Notification with ID {Id} not found .", id);
                return NotFound("Notification not found");

            }
            notification.IsRead = true;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"{notification.Id} is marked as read");
            return Ok(new
            {
                Success = true,
                Message = "Notification marked as read"
            });

        }
            
    }
}
