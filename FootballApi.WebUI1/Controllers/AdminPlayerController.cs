using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces;
using FootballApi.Application.Interfaces.Football;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AdminPlayerController : Controller
{
    private readonly IPlayerService _playerService;
    private readonly ITeamService _teamService; // Takım servisi

    public AdminPlayerController(IPlayerService playerService, ITeamService teamService)
    {
        _playerService = playerService;
        _teamService = teamService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            // Veritabanından oyuncu listesini çek
            var players = await _playerService.GetAllPlayersAsync();
            return View(players);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "Oyuncu verileri alınırken bir hata oluştu.";
            ViewBag.ErrorDetails = ex.Message;
            return View(new List<PlayerDto>());
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PlayerDto playerDto)
    {
        if (ModelState.IsValid)
        {
            await _playerService.AddPlayerAsync(playerDto);
            return RedirectToAction("Index");
        }
        return View(playerDto);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _playerService.DeletePlayerByIdAsync(id);
            TempData["SuccessMessage"] = "Oyuncu başarıyla silindi.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Silme işlemi sırasında bir hata oluştu: {ex.Message}";
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
            {
                TempData["ErrorMessage"] = "Oyuncu bulunamadı.";
                return RedirectToAction("Index");
            }
            var teams = await _teamService.GetAllTeamsAsync(); // Tüm takımları al
            ViewBag.Teams = teams.Select(t => new { t.Id, t.Name }).ToList(); // Takımları ViewBag'e gönder
            return View(player);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PlayerDto playerDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _playerService.UpdatePlayerAsync(playerDto);
                TempData["SuccessMessage"] = "Oyuncu başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Güncelleme sırasında bir hata oluştu: {ex.Message}";
            }
        }
        return View(playerDto);
    }

    // Transfer Sayfasını Döndürür
    [HttpGet]
    public async Task<IActionResult> Transfer()
    {
        try
        {
            var players = await _playerService.GetAllPlayersAsync();
            var teams = await _teamService.GetAllTeamsAsync();

            ViewBag.Players = players;
            ViewBag.Teams = teams;

            return View();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Veriler alınırken bir hata oluştu: {ex.Message}";
            return RedirectToAction("Index");
        }
    }

    // Transfer İşlemini Gerçekleştirir
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
        return RedirectToAction("Transfer");
    }
}
