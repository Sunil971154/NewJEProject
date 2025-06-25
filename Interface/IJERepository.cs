using NewjeProject.Models;

namespace NewjeProject.Interface
{
    public interface IJERepository
    {

        Task<List<JournalEntry>> GetAll();  
        Task<JournalEntry> GetById(int id);
        Task<JournalEntry> SaveEntry(JournalEntry entry);
        Task<JournalEntry> UpdateById(int id, JournalEntry entry);
        Task<bool> DeleteById(int id);

    }
}
