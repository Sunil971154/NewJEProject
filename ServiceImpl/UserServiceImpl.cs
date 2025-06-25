using NewjeProject.Data;
using NewjeProject.Models;
using NewjeProject.Interface;
using Microsoft.EntityFrameworkCore;

namespace NewjeProject.ServiceImpl
{
    public class UserServiceImpl : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserServiceImpl(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddNewUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            // EF is already tracking the user (fetched from DB), so just save
            await _context.SaveChangesAsync();
        }


        public async Task<List<User>> GetAllUser()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> FindByUserName(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }


        public async Task DeleteById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<User?> FindByUserName(string userName)
        {
            return await _context.Users
                // JournalEntries navigation property ko eager loading ke zariye fetch karte hain
                // Taaki user ke saath uski saari journal entries bhi mil jaayein
                .Include(u => u.JournalEntries)

                // userName ke base par first matching user ko laate hain
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
