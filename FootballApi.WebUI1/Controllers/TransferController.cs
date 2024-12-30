using FootballApi.Application.Interfaces;
using FootballApi.Application.Interfaces.Football;
using FootballApi.Services;
using Microsoft.AspNetCore.Mvc;

using FootballApi.WebUI1.ViewModels;

namespace FootballApi.WebUI1.Controllers
{
    public class TransferController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;
  

        public TransferController(IPlayerService playerService, ITeamService teamService)
        {
            _playerService = playerService;
            _teamService = teamService;
       
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Oyuncuları ve takımları ViewBag'e atayın
            var players = await _playerService.GetAllPlayersAsync();
            var teams = await _teamService.GetAllTeamsAsync();

            // ViewBag'e doğru türde değerler atandığından emin olun
            ViewBag.Players = players; // IEnumerable<PlayerDto>
            ViewBag.Teams = teams; // IEnumerable<TeamDto>

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(int playerId, int newTeamId)
        {
            try
            {
                await _playerService.TransferPlayerAsync(playerId, newTeamId);
                TempData["SuccessMessage"] = "Oyuncu başarıyla transfer edildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Transfer işlemi sırasında bir hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

      


    }
}
