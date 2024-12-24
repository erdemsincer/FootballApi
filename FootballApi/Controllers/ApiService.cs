using FootballApi.Application.Interfaces;
using FootballApi.Application.Interfaces.Football;
using FootballApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FootballApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiService : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IRepository _repository;

        public ApiService(IRepository repository, IPlayerService playerService)
        {
            _repository = repository;
            _playerService = playerService;
        }

        [HttpGet("sync")]
        public async Task<IActionResult> SyncPlayers()
        {
            try
            {
                // API'den oyuncu verilerini al
                var playersDto = await _playerService.GetPlayersFromApiAsync();

                // DTO'ları veri modeline dönüştür
                var players = playersDto.Select(dto => new Player
                {
                    Name = dto.Name,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Age = dto.Age,
                    BirthDate = dto.BirthDate,
                    BirthPlace = dto.BirthPlace,
                    BirthCountry = dto.BirthCountry,
                    Nationality = dto.Nationality,
                    Height = dto.Height,
                    Weight = dto.Weight,
                    Number = dto.Number,
                    Position = dto.Position,
                    Photo = dto.Photo,
                     TeamId = dto.TeamId
                });

                // Veritabanına ekle
                await _repository.AddPlayersAsync(playersDto);

                return Ok(new { Message = "Players synchronized successfully" });
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajı döndür
                return StatusCode(500, new { Message = "An error occurred during synchronization.", Details = ex.Message });
            }
        }
    }
}
