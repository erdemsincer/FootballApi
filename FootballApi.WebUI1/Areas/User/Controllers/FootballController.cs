using FootballApi.Application.Interfaces.Football;
using Microsoft.AspNetCore.Mvc;

namespace FootballApi.WebUI1.Areas.User.Controllers
{
    [Area("User")]
    public class FootballController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly IPlayerStatisticService _playerStatisticService;

        public FootballController(IPlayerService playerService, IPlayerStatisticService playerStatisticService)
        {
            _playerService = playerService;
            _playerStatisticService = playerStatisticService;
        }

        // Futbolcu listesini getirir
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var players = await _playerService.GetAllPlayersAsync();

            if (players == null || !players.Any())
            {
                ViewBag.ErrorMessage = "Herhangi bir futbolcu bulunamadı.";
                return View();
            }

            return View(players);
        }

        // Karşılaştırma sayfası için oyuncu listesini getirir
        [HttpGet]
        public async Task<IActionResult> Compare()
        {
            var players = await _playerService.GetAllPlayersAsync();

            if (players == null || !players.Any())
            {
                ViewBag.ErrorMessage = "Karşılaştırma yapmak için yeterli futbolcu bulunamadı.";
                return RedirectToAction("Index");
            }

            return View(players);
        }

        // Karşılaştırma sonucunu gösterir
        [HttpGet]
        public async Task<IActionResult> CompareResult(int player1Id, int player2Id)
        {
            try
            {
                // Oyuncuların istatistiklerini çek
                var player1Stats = await _playerStatisticService.GetStatisticByPlayerIdAsync(player1Id);
                var player2Stats = await _playerStatisticService.GetStatisticByPlayerIdAsync(player2Id);

                // Eğer oyunculardan biri bulunamazsa hata döndür
                if (player1Stats == null || player2Stats == null)
                {
                    ViewBag.ErrorMessage = "Seçilen oyunculardan biri veya her ikisi bulunamadı.";
                    return RedirectToAction("Compare");
                }

                // Oyuncu istatistiklerini View'e gönder
                return View(Tuple.Create(player1Stats, player2Stats));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Karşılaştırma sırasında bir hata oluştu: {ex.Message}";
                return RedirectToAction("Compare");
            }
        }
    }
}
