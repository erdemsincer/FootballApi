using FootballApi.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Application.Interfaces
{
    public interface ILeagueService
    {
        Task<IEnumerable<LeagueDto>> GetAllAsync();
        Task<LeagueDto> GetByIdAsync(int id);
        Task AddAsync(LeagueDto leagueDto);
        Task UpdateAsync(LeagueDto leagueDto);
        Task DeleteAsync(int id);
    }
}
