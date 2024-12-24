using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces;

namespace FootballApi.Services
{
    public class TeamService:ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _teamRepository.GetAllTeamsAsync();
            return teams.Select(t => new TeamDto
            {
                Id = t.Id,
                Name = t.Name,
                Country = t.Country,
                Stadium = t.Stadium,
                Coach = t.Coach
            });
        }
    }
}
