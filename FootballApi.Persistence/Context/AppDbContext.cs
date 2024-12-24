using FootballApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Model.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // DbSet tanımları
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Team> Teams { get; set; }




    }
}
