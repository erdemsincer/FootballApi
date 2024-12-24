using FootballApi.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballApi.Application.Interfaces
{
    public interface IRepository
    {
        Task AddPlayersAsync(IEnumerable<PlayerDto> players);
        Task<IEnumerable<PlayerDto>> GetAllPlayersAsync();

    }
}
