using Microsoft.EntityFrameworkCore;
using NotificationSystem.Models;
namespace NotificationSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }
    }
}
