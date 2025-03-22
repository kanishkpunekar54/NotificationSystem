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

        public NotificationSystemController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] string message)
        {
            var notification = new Notification { Message = message };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
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
            return await _context.Notifications.ToListAsync();
        }

        [HttpPut("{id}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound("Notification not found");

            }
            notification.IsRead = true;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Success = true,
                Message = "Notification marked as read"
            });

        }
            
    }
}
