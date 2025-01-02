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
            try
            {
                return await _dbContext.PlayerStatistics.Include(ps => ps.Player).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("İstatistikler alınırken bir hata oluştu.", ex);
            }
        }

        public async Task<PlayerStatistic> GetStatisticByPlayerIdAsync(int id)
        {
            var statistic = await _dbContext.PlayerStatistics.Include(ps => ps.Player)
                .FirstOrDefaultAsync(ps => ps.PlayerId == id);

            if (statistic == null)
                throw new KeyNotFoundException($"ID {id} ile eşleşen istatistik bulunamadı.");

            return statistic;
        }

        public async Task AddStatisticAsync(PlayerStatistic statistic)
        {
            try
            {
                await _dbContext.PlayerStatistics.AddAsync(statistic);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("İstatistik eklenirken bir hata oluştu.", ex);
            }
        }

        public async Task UpdateStatisticAsync(PlayerStatistic statistic)
        {
            try
            {
                _dbContext.PlayerStatistics.Update(statistic);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("İstatistik güncellenirken bir hata oluştu.", ex);
            }
        }

        public async Task DeleteStatisticAsync(int id)
        {
            var statistic = await _dbContext.PlayerStatistics.FindAsync(id);
            if (statistic == null)
                throw new KeyNotFoundException($"ID {id} ile eşleşen istatistik bulunamadı.");

            try
            {
                _dbContext.PlayerStatistics.Remove(statistic);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("İstatistik silinirken bir hata oluştu.", ex);
            }
        }
    }
}
