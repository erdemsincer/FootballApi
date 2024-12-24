using Microsoft.AspNetCore.Mvc;

namespace FootballApi.WebUI1.Controllers
{
    public class PlayerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
