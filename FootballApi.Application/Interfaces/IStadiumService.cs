using FootballApi.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Application.Interfaces
{
    public interface IStadiumService
    {
        Task<IEnumerable<StadiumDto>> GetAllAsync();
        Task<StadiumDto> GetByIdAsync(int id);
        Task AddAsync(StadiumDto stadiumDto);
        Task UpdateAsync(StadiumDto stadiumDto);
        Task DeleteAsync(int id);
    }
}
