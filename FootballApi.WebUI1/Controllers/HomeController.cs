using FootballApi.WebUI1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Context;
using System.Diagnostics;
using System.Linq;

namespace FootballApi.WebUI1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Ýstatistik verileri
            var totalPlayers = _context.Players.Count();
            var totalTeams = _context.Teams.Count();
            var totalLeagues = _context.Leagues.Count();

            // Son eklenen oyuncular
            var recentPlayers = _context.Players
                .OrderByDescending(p => p.Id)
                .Take(5)
                .Select(p => p.Name)
                .ToList();

            // ViewBag ile verileri gönder
            ViewBag.TotalPlayers = totalPlayers;
            ViewBag.TotalTeams = totalTeams;
            ViewBag.TotalLeagues = totalLeagues;
            ViewBag.RecentPlayers = recentPlayers;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
