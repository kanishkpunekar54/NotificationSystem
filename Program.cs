using Microsoft.EntityFrameworkCore;
using NotificationSystem.Data;

namespace NotificationSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //database COnnection
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=POOLW42NLPC2020\\SQLEXPRESS;Database=NotificationDB;Trusted_Connection=True;TrustServerCertificate=True");
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
