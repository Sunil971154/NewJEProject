using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NewjeProject.Models
{
    public class JournalEntry
    {

        public int Id { get; set; }      // case matter nahi karta letter chota bada ho dakta hai       
        public string Title { get; set; }
        public string Content { get; set; }
        public DateOnly LocalDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);  // Date and time of the entry
        public TimeOnly LocalTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        public int UserId { get; set; } // Database me actual foreign key column

        [ForeignKey("UserId")]
        [ValidateNever]  
        [JsonIgnore]
        public User User { get; set; } //C# object ke through related user ka access Navigation property
         

    }
}
