using FootballApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Application.Interfaces.Football
{
    public interface IPlayerStatisticService
    {
        Task<IEnumerable<PlayerStatistic>> GetAllStatisticsAsync();
        Task<PlayerStatistic> GetStatisticByPlayerIdAsync(int id);
        Task AddStatisticAsync(PlayerStatistic statistic);
        Task UpdateStatisticAsync(PlayerStatistic statistic);
        Task DeleteStatisticAsync(int id);

    }
}
