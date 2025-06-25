using Microsoft.IdentityModel.Tokens;
using NewjeProject.Models;
using NewjeProject.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NewjeProject.Interface;

public class AuthService : IAuthRepository   
{
    private readonly AppDbContext _appDBContext;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _appDBContext = context;
        _configuration = configuration;
    }

    public string Login(LoginModel login)
    {
        // Step 1: Username aur password khali na ho
        if (login.Username == null || login.Password == null)
            throw new Exception("Username ya Password khali hai");

        // Step 2: User ko database me dhundo
        var user = _appDBContext.Users
            .FirstOrDefault(u => u.UserName == login.Username && u.Password == login.Password);

        // Step 3: Agar user nahi mila to error
        if (user == null)
            throw new Exception("Username ya Password galat hai");

        // Step 4: Token return karo
        return GenerateJwtToken(user);
    }


    private string GenerateJwtToken(User user)
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
