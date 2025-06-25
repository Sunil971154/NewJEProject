using NewjeProject.Models;

namespace NewjeProject.Interface
{
    public interface IAuthRepository
    {
        string Login(LoginModel loginRequest);
    }


}
