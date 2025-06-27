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
    private readonly IJwtService _jwtService;

    public AuthService(AppDbContext context, IJwtService jwtService)
    {
        _appDBContext = context;
        _jwtService = jwtService;
    }

    public string Login(LoginModel login)
    {
        // Step 1: Username aur password khali na ho
        if (login.Username == null || login.Password == null)
            throw new Exception("Username ya Password khali hai");

        // Step 2: User find karo by username
        var user = _appDBContext.Users
            .FirstOrDefault(u => u.UserName == login.Username);

        // Step 3: User exist karta hai aur password match karta hai?
        if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            throw new Exception("Username ya Password galat hai");

        // Step 4: Token generate aur return
        return _jwtService.GenerateJwtToken(user);
    }
     
   

}
