using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces;
using FootballApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        // Tüm takımları ve oyuncuları getirir
        [HttpGet]
        public async Task<IActionResult> GetAllWithPlayers()
        {
            var teams = await _teamRepository.GetAllWithPlayersAsync();
            return Ok(teams);
        }

        // Belirli bir takımı ve oyuncuları getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdWithPlayers(int id)
        {
            var team = await _teamRepository.GetByIdWithPlayersAsync(id);
            if (team == null)
            {
                return NotFound("Team not found.");
            }

            return Ok(team);
        }

        // Yeni takım ekler
        [HttpPost]
        public async Task<IActionResult> AddTeam([FromBody] TeamDto teamDto)
        {
            if (teamDto == null)
            {
                return BadRequest("Team data is null.");
            }

            var team = new Team
            {
                Id = teamDto.Id,
                Name = teamDto.Name,
                Country = teamDto.Country,
                Stadium = teamDto.Stadium,
                Coach = teamDto.Coach
            };

            await _teamRepository.AddAsync(team);
            return CreatedAtAction(nameof(GetByIdWithPlayers), new { id = team.Id }, team);
        }

        // Takımı günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] TeamDto teamDto)
        {
            if (id != teamDto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var team = new Team
            {
                Id = teamDto.Id,
                Name = teamDto.Name,
                Country = teamDto.Country,
                Stadium = teamDto.Stadium,
                Coach = teamDto.Coach
            };

            await _teamRepository.UpdateAsync(team);
            return NoContent();
        }

        // Takımı siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _teamRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
