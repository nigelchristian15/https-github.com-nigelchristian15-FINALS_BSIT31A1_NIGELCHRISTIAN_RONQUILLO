using Microsoft.AspNetCore.Mvc;
using DailyJournalSystem_Fixed.Services.Interfaces;
using DailyJournalSystem_Fixed.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace DailyJournalSystem_Fixed.Web.Controllers
{
    [Authorize]
    public class JournalController : Controller
    {
        private readonly IJournalService _service;
        private readonly UserManager<IdentityUser> _userManager;

        public JournalController(IJournalService service, UserManager<IdentityUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var entries = await _service.GetByUserAsync(user.Id);
            if (date.HasValue)
            {
                entries = entries.Where(e => e.Date.Date == date.Value.Date);
                ViewData["FilterDate"] = date.Value.ToString("yyyy-MM-dd");
            }
            return View(entries);
        }

        public IActionResult Create() => View(new JournalEntry { Date = DateTime.UtcNow });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JournalEntry model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();
            model.UserId = user.Id;
            await _service.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entry = await _service.GetByIdAsync(id);
            if (entry == null) return NotFound();
            var user = await _userManager.GetUserAsync(User);
            if (user == null || entry.UserId != user.Id) return Forbid();
            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JournalEntry model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            var entry = await _service.GetByIdAsync(id);
            if (entry == null) return NotFound();
            var user = await _userManager.GetUserAsync(User);
            if (user == null || entry.UserId != user.Id) return Forbid();
            entry.Content = model.Content;
            entry.Date = model.Date;
            await _service.UpdateAsync(entry);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var entry = await _service.GetByIdAsync(id);
            if (entry == null) return NotFound();
            var user = await _userManager.GetUserAsync(User);
            if (user == null || entry.UserId != user.Id) return Forbid();
            return View(entry);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = await _service.GetByIdAsync(id);
            if (entry == null) return NotFound();
            var user = await _userManager.GetUserAsync(User);
            if (user == null || entry.UserId != user.Id) return Forbid();
            return View(entry);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entry = await _service.GetByIdAsync(id);
            if (entry == null) return NotFound();
            var user = await _userManager.GetUserAsync(User);
            if (user == null || entry.UserId != user.Id) return Forbid();
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
