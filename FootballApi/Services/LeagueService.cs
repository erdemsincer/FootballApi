using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces;
using FootballApi.Domain.Entities;

namespace FootballApi.Services
{
    public class LeagueService:ILeagueService
    {
        private readonly ILeagueRepository _leagueRepository;

        public LeagueService(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        public async Task<IEnumerable<LeagueDto>> GetAllAsync()
        {
            var leagues = await _leagueRepository.GetAllAsync();
            return leagues.Select(l => new LeagueDto
            {
                Id = l.Id,
                Name = l.Name,
                Country = l.Country,
                LogoUrl = l.LogoUrl,
                NumberOfTeams = l.NumberOfTeams
            });
        }

        public async Task<LeagueDto> GetByIdAsync(int id)
        {
            var league = await _leagueRepository.GetByIdAsync(id);
            if (league == null) return null;

            return new LeagueDto
            {
                Id = league.Id,
                Name = league.Name,
                Country = league.Country,
                LogoUrl = league.LogoUrl,
                NumberOfTeams = league.NumberOfTeams
            };
        }

        public async Task AddAsync(LeagueDto leagueDto)
        {
            var league = new League
            {
                Name = leagueDto.Name,
                Country = leagueDto.Country,
                LogoUrl = leagueDto.LogoUrl,
                NumberOfTeams = leagueDto.NumberOfTeams
            };

            await _leagueRepository.AddAsync(league);
        }

        public async Task UpdateAsync(LeagueDto leagueDto)
        {
            var league = new League
            {
                Id = leagueDto.Id,
                Name = leagueDto.Name,
                Country = leagueDto.Country,
                LogoUrl = leagueDto.LogoUrl,
                NumberOfTeams = leagueDto.NumberOfTeams
            };

            await _leagueRepository.UpdateAsync(league);
        }

        public async Task DeleteAsync(int id)
        {
            await _leagueRepository.DeleteAsync(id);
        }
    }
}
