using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FootballApi.WebUI1.Controllers
{
    public class AdminLeagueController : Controller
    {
        private readonly ILeagueService _leagueService;

        public AdminLeagueController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        public async Task<IActionResult> Index()
        {
            var leagues = await _leagueService.GetAllAsync();
            return View(leagues);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeagueDto leagueDto)
        {
            if (ModelState.IsValid)
            {
                await _leagueService.AddAsync(leagueDto);
                TempData["SuccessMessage"] = "Lig başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Lig eklenirken bir hata oluştu.";
            return View(leagueDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var league = await _leagueService.GetByIdAsync(id);
            if (league == null) return NotFound();

            return View(league);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LeagueDto leagueDto)
        {
            if (ModelState.IsValid)
            {
                await _leagueService.UpdateAsync(leagueDto);
                TempData["SuccessMessage"] = "Lig başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Lig güncellenirken bir hata oluştu.";
            return View(leagueDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _leagueService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Lig başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}
