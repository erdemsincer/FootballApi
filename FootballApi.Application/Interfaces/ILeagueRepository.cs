using FootballApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Application.Interfaces
{
    public interface ILeagueRepository
    {
        Task<IEnumerable<League>> GetAllAsync();
        Task<League> GetByIdAsync(int id);
        Task AddAsync(League league);
        Task UpdateAsync(League league);
        Task DeleteAsync(int id);
    }
}
