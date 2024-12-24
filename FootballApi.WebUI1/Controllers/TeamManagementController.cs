using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces;
using FootballApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FootballApi.WebUI1.Controllers
{
    public class TeamManagementController : Controller
    {
        private readonly ITeamRepository _teamRepository;

        public TeamManagementController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        // Tüm takımları listele
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var teams = await _teamRepository.GetAllWithPlayersAsync();
            return View(teams);
        }

        // Yeni takım ekleme formunu göster
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni takımı ekle
        [HttpPost]
        public async Task<IActionResult> Create(TeamDto teamDto)
        {
            if (ModelState.IsValid)
            {
                var team = new Team
                {
                    Id = teamDto.Id,
                    Name = teamDto.Name,
                    Country = teamDto.Country,
                    Stadium = teamDto.Stadium,
                    Coach = teamDto.Coach
                };

                await _teamRepository.AddAsync(team);
                TempData["SuccessMessage"] = "Takım başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Takım ekleme sırasında bir hata oluştu.";
            return View(teamDto);
        }

        // Takımı düzenleme formunu göster
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var team = await _teamRepository.GetByIdWithPlayersAsync(id);
            if (team == null)
            {
                TempData["ErrorMessage"] = "Takım bulunamadı.";
                return RedirectToAction("Index");
            }

            var teamDto = new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                Country = team.Country,
                Stadium = team.Stadium,
                Coach = team.Coach
            };

            return View(teamDto);
        }

        // Takımı düzenle
        [HttpPost]
        public async Task<IActionResult> Edit(TeamDto teamDto)
        {
            if (ModelState.IsValid)
            {
                var team = new Team
                {
                    Id = teamDto.Id,
                    Name = teamDto.Name,
                    Country = teamDto.Country,
                    Stadium = teamDto.Stadium,
                    Coach = teamDto.Coach
                };

                await _teamRepository.UpdateAsync(team);
                TempData["SuccessMessage"] = "Takım başarıyla güncellendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Takım güncelleme sırasında bir hata oluştu.";
            return View(teamDto);
        }

        // Takımı sil
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _teamRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Takım başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Takım silme sırasında bir hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
