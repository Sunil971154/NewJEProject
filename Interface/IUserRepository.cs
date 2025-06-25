using NewjeProject.Models;

namespace NewjeProject.Interface
{
    public interface IUserRepository
    {

        Task AddNewUser(User user);                            // Create/UpdateAdd commentMore actions
        Task UpdateUser(User user);                            // Create/Update

        Task<List<User>> GetAllUser();                             // Read All
        Task<User?> FindByUserName(Guid id);                       // Read by ID
        Task DeleteById(Guid id);                            // Delete
        Task<User?> FindByUserName(string userName);         // Read by Username







    }
}
