using Microsoft.EntityFrameworkCore;
using NewjeProject.Data;
using NewjeProject.Interface;
using NewjeProject.Models;
 

namespace Revision_Project.ServiceIMPL
{
    public class JERepository : IJERepository
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;


        public JERepository(AppDbContext context, IUserRepository userService)
        {
            _context = context;
            _userRepository = userService;
        }

        public async Task<JournalEntry> SaveEntry(JournalEntry entry)
        {
            try
            {
                _context.JournalEntries.Add(entry);
                await _context.SaveChangesAsync();
                return entry;
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error in SaveEntry: " + ex.Message);
                Console.WriteLine("UserId: " + entry.UserId);
                Console.WriteLine("Title: " + entry.Title);
                Console.WriteLine("User Is Null: " + (entry.User == null));
                throw;
            }
        }

        public async Task<JournalEntry> SaveEntryWithUser(JournalEntry journalEntry, string username)
        {

            var user = await _userRepository.FindByUserName(username);

            journalEntry.UserId = (int)user.Id;   // ✅ Required  
            journalEntry.User = null;

            var saved = await SaveEntry(journalEntry);

            user.JournalEntries ??= new List<JournalEntry>();
            user.JournalEntries.Add(saved);

            await _userRepository.UpdateUser(user);

            return saved;
        }

        public async Task<List<JournalEntry>> FindAll()
        {
            return await _context.JournalEntries.ToListAsync();
        }

        public async Task<JournalEntry> FindById(string id)
        {
            if (!long.TryParse(id, out var entryId))
                return null;

            return await _context.JournalEntries.FindAsync(entryId);
        }

        public async Task DeleteById(string id)
        {
            if (!long.TryParse(id, out var entryId))
                return;

            var entry = await _context.JournalEntries.FindAsync(entryId);
            if (entry != null)
            {
                _context.JournalEntries.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }
    }
}