using FootballApi.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballApi.Application.Interfaces.Football
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetPlayersFromApiAsync(); // API'den oyuncuları alır
        Task<IEnumerable<PlayerDto>> GetAllPlayersAsync();
        Task AddPlayerAsync(PlayerDto playerDto);// Veritabanından oyuncuları alır
        Task<PlayerDto> GetPlayerByIdAsync(int id);
        Task DeletePlayerByIdAsync(int id);
        Task UpdatePlayerAsync(PlayerDto playerDto);
        Task TransferPlayerAsync(int playerId, int newTeamId);

    }
}
