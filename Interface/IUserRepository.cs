using NewjeProject.Models;

namespace NewjeProject.Interface
{
    public interface IUserRepository
    {
        /*
                Task AddNewUser(User user);                            // Create/UpdateAdd commentMore actions

                public Task AddGoogleUser(User user) => AddUser(user, isGoogleUser: true);
                public Task AddNormalUser(User user) => AddUser(user, isGoogleUser: false);

                Task UpdateUser(User user);                            // Create/Update

                Task<List<User>> GetAllUser();                             // Read All
                Task<User?> FindByUserName(Guid id);                       // Read by ID

                Task<User?> FindByUserName(string userName);         // Read by Username

                Task<User?> FindById(int id);
                Task DeleteById(int id);


                */

       
            Task AddNewUser(User user, bool isGoogleUser = false);         // 🔁 Common method
            
            //Task AddNormalUser(User user);                              // Manual signup

            Task UpdateUser(User user);
            Task<List<User>> GetAllUser();
            Task<User?> FindByUserName(Guid id);
            Task<User?> FindByUserName(string userName);
            Task<User?> FindById(int id);
            Task DeleteById(int id);
         



    }
}
