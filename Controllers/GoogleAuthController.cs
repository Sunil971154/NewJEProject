using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewjeProject.Models;
using NewjeProject.Data;
using NewjeProject.Interface;

namespace NewjeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleAuthController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _iuserService;

        public GoogleAuthController(AppDbContext  dbContext , IJwtService jwtService, IUserRepository iuserService)
        {
            _context = dbContext;
            _jwtService = jwtService;
            _iuserService = iuserService;

        }


        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleTokenRequest code)
        {
            try
            {
                // 1. Verify Google Token
                var payload = await GoogleJsonWebSignature.ValidateAsync(code.Token);
                 
                // 2. Check if user already exists by email
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == payload.Email);
                
                if(user!=null)
                Console.WriteLine($"User found: Id = {user.Id}, Name = {user.Name}, Email = {user.Email}");
                // 3. If user does not exist, create new user
                if (user == null)
                {
                    user = new User
                    {
                        UserName = payload.Name,
                        Email = payload.Email,
                        Name = payload.Name,
                        // Picture = payload.Picture // optional
                    };
                    await _iuserService.AddNewUser(user, true); // ✅ now user is ready                   
                }

                // 4. Generate JWT Token
                var token = _jwtService.GenerateJwtToken(user); // your custom method

                // 5. Return JWT Token
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "Invalid Google token", error = ex.Message });
            }
        }

    }
}
