using Microsoft.AspNetCore.Mvc;

namespace ClientOvertime.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
