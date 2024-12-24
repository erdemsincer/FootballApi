using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces;
using FootballApi.Application.Interfaces.Football;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AdminPlayerController : Controller
{
    private readonly IPlayerService _playerService;


    public AdminPlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
  
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            // Veritabanından oyuncu listesini çek
            var players = await _playerService.GetAllPlayersAsync();

            // View'e oyuncuları gönder
            return View(players);
        }
        catch (Exception ex)
        {
            // Hata durumunda kullanıcıya bilgi ver
            ViewBag.ErrorMessage = "Oyuncu verileri alınırken bir hata oluştu.";
            ViewBag.ErrorDetails = ex.Message;
            return View(new List<PlayerDto>()); // Boş liste gönder
        }
    }

    public IActionResult Create()
    {
        return View(); // Form sayfasını döndür
    }

    [HttpPost]
    public async Task<IActionResult> Create(PlayerDto playerDto)
    {
        if (ModelState.IsValid)
        {
            // Yeni oyuncu verisini kaydet
            await _playerService.AddPlayerAsync(playerDto);
            return RedirectToAction("Index");
        }

        return View(playerDto); // Hata varsa formu geri döndür
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
            return View(player);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            return RedirectToAction("Index");
        }
    }

    // Düzenleme İşlemini Kaydeder
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
   
    }



