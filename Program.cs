
using Microsoft.EntityFrameworkCore;
using NewjeProject.Data;
using NewjeProject.Interface;
using NewjeProject.ServiceImpl;

namespace NewjeProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));// connection string app setting me bana lo 


            // Add services to the container.
            builder.Services.AddScoped<IJERepository, JERepository>();
            builder.Services.AddScoped<IUserRepository, UserServiceImpl>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            Console.WriteLine(" Program Stared Now ");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
