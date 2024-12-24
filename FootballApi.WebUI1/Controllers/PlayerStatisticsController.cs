
using FootballApi.Application.Interfaces.Football;
using FootballApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FootballApi.WebUI.Controllers
{
    public class PlayerStatisticsController : Controller
    {
        private readonly IPlayerStatisticService _statisticService;

        public PlayerStatisticsController(IPlayerStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        // Tüm istatistikleri listeleme
        public async Task<IActionResult> Index()
        {
            var statistics = await _statisticService.GetAllStatisticsAsync();
            return View(statistics);
        }

        // Yeni istatistik ekleme - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View(new PlayerStatistic());
        }

        // Yeni istatistik ekleme - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerStatistic statistic)
        {
            try
            {
                await _statisticService.AddStatisticAsync(statistic);
                TempData["SuccessMessage"] = "İstatistik başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ekleme sırasında bir hata oluştu: {ex.Message}";
            }

            return View(statistic);
        }

        // İstatistik düzenleme - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var statistic = await _statisticService.GetStatisticByPlayerIdAsync(id);
            if (statistic == null)
            {
                TempData["ErrorMessage"] = "Düzenlemek istediğiniz istatistik bulunamadı.";
                return RedirectToAction("Index");
            }

            return View(statistic);
        }


        // İstatistik düzenleme - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlayerStatistic updatedStatistic)
        {
            try
            {
                await _statisticService.UpdateStatisticAsync(updatedStatistic);
                TempData["SuccessMessage"] = "İstatistik başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Güncelleme sırasında bir hata oluştu: {ex.Message}";
            }

            return View(updatedStatistic);
        }


        // İstatistik silme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _statisticService.DeleteStatisticAsync(id);
                TempData["SuccessMessage"] = "İstatistik başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Silme sırasında bir hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
