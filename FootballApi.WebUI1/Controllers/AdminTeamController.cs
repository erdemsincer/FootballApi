using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballApi.WebUI1.Controllers
{
    public class AdminTeamController : Controller
    {
        private readonly IRepository _repository;

        public AdminTeamController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Repository'den oyuncuları çek
                var players = await _repository.GetAllPlayersAsync();

                // Player -> PlayerDto dönüşümü
                var playerDtos = players.Select(player => new PlayerDto
                {
                    Id = player.Id,
                    Name = player.Name,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    Age = player.Age,
                    BirthDate = player.BirthDate,
                    BirthPlace = player.BirthPlace,
                    BirthCountry = player.BirthCountry,
                    Nationality = player.Nationality,
                    Height = player.Height,
                    Weight = player.Weight,
                    Number = player.Number,
                    Position = player.Position,
                    Photo = player.Photo
                });

                // View'e gönder
                return View(playerDtos);
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya bilgi ver
                ViewBag.ErrorMessage = "Takım verileri alınırken bir hata oluştu.";
                ViewBag.ErrorDetails = ex.Message;
                return View(new List<PlayerDto>()); // Boş liste gönder
            }
        }
    }
}
