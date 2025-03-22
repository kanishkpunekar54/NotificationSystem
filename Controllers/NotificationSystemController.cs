using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationSystem.Data;
using NotificationSystem.Models;

namespace NotificationSystem.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("/api/notifications")]
    [ApiController]
    public class NotificationSystemController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<NotificationSystemController> _logger;

        public NotificationSystemController(AppDbContext context,ILogger<NotificationSystemController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] string message)
        {
            _logger.LogInformation("Received request to send notification :",message);
            var notification = new Notification { Message = message };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Sending notification: {notification}");
            return Ok(
                   new
                   {
                       Success = true,
                       Message = "Notification Sent Successfully",
                       Notification = notification
                       

                   }
                );


        }


        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetAllNotifications()
        {
            _logger.LogInformation("Fetching all notifications");
            var notifications = await _context.Notifications.ToListAsync();
            _logger.LogInformation($"{notifications.Count} notifications");
            return await _context.Notifications.ToListAsync();
        }

        [HttpPut("{id}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            _logger.LogInformation("Received request to mark notification {Id} as read ",id);
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound("Notification not found");

            }
            notification.IsRead = true;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"{notification.Id} is read");
            return Ok(new
            {
                Success = true,
                Message = "Notification marked as read"
            });

        }
            
    }
}
