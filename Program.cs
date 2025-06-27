
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewjeProject.Data;
using NewjeProject.Interface;
using NewjeProject.ServiceImpl;
using Revision_Project.ServiceIMPL;
using System.Text;

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
            builder.Services.AddScoped<IAuthRepository, AuthService>();
            builder.Services.AddScoped<IJwtService, JwtService>();

            // Add Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            // 🔹 Step 1: Register CORS service
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000") // your React app URL
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // Optional, only if you're sending cookies/auth headers
                });
            });

            

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            
            // 🔹 Step 2: Use CORS before routing/mvc
            app.UseCors("AllowFrontend"); // 👈 Must come BEFORE UseRouting

            Console.WriteLine(" Program Stared Now ");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
             

            app.MapControllers();

            app.Run();
        }
    }
}
