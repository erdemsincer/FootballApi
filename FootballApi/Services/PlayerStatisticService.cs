using FootballApi.Application.Interfaces.Football;
using FootballApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Context;


namespace FootballApi.Services
{
    public class PlayerStatisticService : IPlayerStatisticService
    {
        private readonly AppDbContext _dbContext;

        public PlayerStatisticService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerStatistic>> GetAllStatisticsAsync()
        {
            return await _dbContext.PlayerStatistics.Include(ps => ps.Player).ToListAsync();
        }

        public async Task<PlayerStatistic> GetStatisticByPlayerIdAsync(int playerId)
        {
            return await _dbContext.PlayerStatistics.Include(ps => ps.Player)
                .FirstOrDefaultAsync(ps => ps.PlayerId == playerId);
        }

        public async Task AddStatisticAsync(PlayerStatistic statistic)
        {
            await _dbContext.PlayerStatistics.AddAsync(statistic);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateStatisticAsync(PlayerStatistic statistic)
        {
            _dbContext.PlayerStatistics.Update(statistic);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStatisticAsync(int id)
        {
            var statistic = await _dbContext.PlayerStatistics.FindAsync(id);
            if (statistic != null)
            {
                _dbContext.PlayerStatistics.Remove(statistic);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
