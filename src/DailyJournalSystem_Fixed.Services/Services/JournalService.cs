using System.Threading.Tasks;
using DailyJournalSystem_Fixed.Services.Interfaces;
using DailyJournalSystem_Fixed.Infrastructure.Data;
using DailyJournalSystem_Fixed.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DailyJournalSystem_Fixed.Services.Services
{
    public class JournalService : IJournalService
    {
        private readonly ApplicationDbContext _db;
        public JournalService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<JournalEntry> CreateAsync(JournalEntry entry)
        {
            _db.JournalEntries.Add(entry);
            await _db.SaveChangesAsync();
            return entry;
        }

        public async Task<JournalEntry?> GetByIdAsync(int id)
        {
            return await _db.JournalEntries.FindAsync(id);
        }

        public async Task<IEnumerable<JournalEntry>> GetByUserAsync(string userId)
        {
            return await _db.JournalEntries
                .Where(j => j.UserId == userId)
                .OrderByDescending(j => j.Date)
                .ToListAsync();
        }

        public async Task UpdateAsync(JournalEntry entry)
        {
            _db.JournalEntries.Update(entry);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _db.JournalEntries.FindAsync(id);
            if (e != null)
            {
                _db.JournalEntries.Remove(e);
                await _db.SaveChangesAsync();
            }
        }
    }
}
