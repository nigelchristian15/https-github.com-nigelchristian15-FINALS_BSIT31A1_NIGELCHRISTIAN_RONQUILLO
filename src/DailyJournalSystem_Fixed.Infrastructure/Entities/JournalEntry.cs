using System;
using System.ComponentModel.DataAnnotations;

namespace DailyJournalSystem_Fixed.Infrastructure.Entities
{
    public class JournalEntry
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
