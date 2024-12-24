using Microsoft.AspNetCore.Mvc;

namespace FootballApi.WebUI1.Controllers
{
    public class AdminFootballController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
