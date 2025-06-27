using NewjeProject.Models;

namespace NewjeProject.Interface
{
      public interface IJwtService
        {
            string GenerateJwtToken(User user);
        }

    
}
