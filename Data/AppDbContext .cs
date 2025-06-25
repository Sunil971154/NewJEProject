using Microsoft.EntityFrameworkCore;
using NewjeProject.Models;

namespace NewjeProject.Data
{
    public class AppDbContext : DbContext
    {
        /*
       ✅ 1. AppDbContext(DbContextOptions<AppDbContext> options)
         Isme DbContextOptions<AppDbContext> parameter diya gaya hai, jisme database connection aur configuration ki details hoti hain.
         Iska matlab: Jab EF Core aapka DbContext banata hai, toh wo yahan se options pass karega (e.g., SQL Server ka connection string, lazy loading config, etc.).

        ✅ 2. : base(options)
            Ye DbContext base class ka constructor call karta hai.
            EF Core ke DbContext class me internal logic hota hai jo options ke basis par database ke saath connection banata hai, migrations handle karta hai, etc.
            Agar aap base(options) nahi likhenge, toh EF Core ko pata hi nahi chalega ki kaunsi database configuration use karni hai.
         */
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        /*
              | Feature                            | Purpose                                                                       |
              | ---------------------------------- | ----------------------------------------------------------------------------- |
              | `HasIndex(...).IsUnique()`         | `UserName` को unique बना रहा है                                               |
              | `HasMany(...).WithOne(...)`        | `User` और `JournalEntry` के बीच 1-to-many relationship बना रहा है             |
              | `HasForeignKey(j => j.UserId)`     | Foreign key define कर रहा है                                                  |
              | `OnDelete(DeleteBehavior.Cascade)` | User delete होने पर उसके journal entries को भी automatically delete कर रहा है |


         */

        /* DB me Table banegi JournalEntries name se
         * 👉 यह property आपके DbContext में एक table की तरह काम करेगी, जिसका नाम है JournalEntries
           👉 और हर row का type है JournalEntry*/
      
        
        public DbSet<JournalEntry> JournalEntries { get; set; }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Configure Cascade Delete between User and JournalEntry
            modelBuilder.Entity<JournalEntry>()
                .HasOne(j => j.User)
                .WithMany(u => u.JournalEntries)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
