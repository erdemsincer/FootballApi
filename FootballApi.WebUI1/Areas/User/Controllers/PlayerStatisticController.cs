using FootballApi.Application.Interfaces.Football;
using Microsoft.AspNetCore.Mvc;

namespace FootballApi.WebUI1.Areas.User.Controllers
{
    [Area("User")]
    public class PlayerStatisticController : Controller
    {
        private readonly IPlayerStatisticService _statisticService;

        public PlayerStatisticController(IPlayerStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // API veya servis üzerinden istatistikleri çek
            var statistics = await _statisticService.GetAllStatisticsAsync();

            // Eğer veri yoksa hata mesajı döndür
            if (statistics == null || !statistics.Any())
            {
                ViewBag.ErrorMessage = "Herhangi bir istatistik bulunamadı.";
                return View();
            }

            return View(statistics); // Verileri View'e gönder
        }
    }
}
