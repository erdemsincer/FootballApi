using FootballApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Application.Interfaces
{
    public interface IStadiumRepository
    {
        Task<IEnumerable<Stadium>> GetAllAsync();
        Task<Stadium> GetByIdAsync(int id);
        Task AddAsync(Stadium stadium);
        Task UpdateAsync(Stadium stadium);
        Task DeleteAsync(int id);
    }
}
