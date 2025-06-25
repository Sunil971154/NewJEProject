using NewjeProject.Models;

namespace NewjeProject.Interface
{
    public interface IJERepository
    {
         
        Task<JournalEntry> SaveEntryWithUser(JournalEntry entry, string userName);
        Task<List<JournalEntry>> FindAll(); 
        Task<JournalEntry> FindById(string id);
        Task DeleteById(string id);



    }
}
