using Microsoft.IdentityModel.Tokens;
using NewjeProject.Interface;
using NewjeProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewjeProject.ServiceImpl
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {
            // 🔐 1. Get secret key from configuration and convert to bytes
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // 📝 2. Create signing credentials using HMAC SHA256
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 👤 3. Add user-related claims (info stored inside the token)
            var claims = new[]
            {
        new Claim("Id", user.Id.ToString()),
        new Claim("UserName", user.UserName)
    };

            // 🎟️ 4. Create the JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],      // Who issued the token
                audience: _configuration["Jwt:Audience"],  // Who can use the token
                claims: claims,                            // What info inside token
                expires: DateTime.UtcNow.AddMinutes(60),   // Expiry time (1 hour)
                signingCredentials: creds                  // Signing info
            );

            // 🔁 5. Convert token object to string and return
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
