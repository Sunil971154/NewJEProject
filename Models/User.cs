using System.ComponentModel.DataAnnotations;

namespace NewjeProject.Models
{
    public class User
    {
        [Key]  
        public int Id { get; set; }         // SQL Server में ज्यादातर int primary key होता है, लेकिन आप GUID भी use कर सकते हैं         
        
        [Required]                           // EF Core में सीधे attribute से नहीं होता, नीचे Fluent API में दिखाते हैं
        public string Name { get; set; }
        public string? UserName { get; set; }

        public string Email { get; set; }

        
        public string? Password { get; set; }  // 👈 nullable banado RECOMMENDED for Google Login
        public string? Provider { get; set; }



        // Navigation property       
        public List<JournalEntry> JournalEntries { get; set; } = new();


    }
}
