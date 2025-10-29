using Microsoft.AspNetCore.Mvc;

namespace DailyJournalSystem_Fixed.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
