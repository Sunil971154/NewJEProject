using NewjeProject.Data;
using NewjeProject.Models;
using NewjeProject.Interface;
using Microsoft.EntityFrameworkCore;

namespace NewjeProject.ServiceImpl
{
    public class JERepository : IJERepository
    {
        private readonly AppDbContext _context;


        public JERepository(AppDbContext context) // 🏗️ Constructor jo AppDbContext ko inject karta hai     
        {
            _context = context;
        }


        public async Task<List<JournalEntry>> GetAll()
        {
            return await _context.JournalEntries.ToListAsync(); // 🔁 Saare journal entries list me laao (async)
        }

        public async Task<JournalEntry?> GetById(int id)
        {
            return await _context.JournalEntries.FindAsync(id); // 🔍 ID ke basis par ek entry fetch karo
        }

        public async Task<JournalEntry> SaveEntry(JournalEntry journalEntry)
        {
            _context.JournalEntries.Add(journalEntry); // ➕ Nayi entry ko context me add karo
            await _context.SaveChangesAsync(); // 💾 Database me save karo (async)
            return journalEntry; // ✅ Return karo saved object
        }

        public async Task<JournalEntry?> UpdateById(int id, JournalEntry entry)
        {
            var existing = await _context.JournalEntries.FindAsync(id); // 🔍 ID se existing entry dhundo
            if (existing == null) return null; // ❌ Nahi mili to null return karo

            existing.Title = entry.Title; // 📝 Title update karo
            existing.Content = entry.Content; // 📝 Content update karo
            await _context.SaveChangesAsync(); // 💾 Changes save karo database me
            return existing; // ✅ Updated entry return karo
        }

        public async Task<bool> DeleteById(int id)
        {
            var entry = await _context.JournalEntries.FindAsync(id); // 🔍 Entry dhundo ID ke through
            if (entry == null) return false; // ❌ Agar nahi mili to false return karo

            _context.JournalEntries.Remove(entry); // 🗑️ Entry remove karo context se
            await _context.SaveChangesAsync(); // 💾 Save changes to delete from DB
            return true; // ✅ True return karo after delete
        }


    }
}
