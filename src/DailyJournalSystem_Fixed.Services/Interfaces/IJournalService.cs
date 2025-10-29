using System.Collections.Generic;
using System.Threading.Tasks;
using DailyJournalSystem_Fixed.Infrastructure.Entities;

namespace DailyJournalSystem_Fixed.Services.Interfaces
{
    public interface IJournalService
    {
        Task<JournalEntry> CreateAsync(JournalEntry entry);
        Task<IEnumerable<JournalEntry>> GetByUserAsync(string userId);
        Task<JournalEntry?> GetByIdAsync(int id);
        Task UpdateAsync(JournalEntry entry);
        Task DeleteAsync(int id);
    }
}
