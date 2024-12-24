using FootballApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Application.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllWithPlayersAsync();
        Task<Team> GetByIdWithPlayersAsync(int id);
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task AddAsync(Team team);
        Task UpdateAsync(Team team);
        Task DeleteAsync(int id);
    }
}
