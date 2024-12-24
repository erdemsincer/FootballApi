using FootballApi.Application.Interfaces;
using FootballApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballApi.Persistence.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllWithPlayersAsync()
        {
            return await _context.Teams.Include(t => t.Players).ToListAsync();
        }

        public async Task<Team> GetByIdWithPlayersAsync(int id)
        {
            return await _context.Teams.Include(t => t.Players)
                                       .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync(); // Veritabanına kaydediliyor
        }

        public async Task UpdateAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync(); // Veritabanına kaydediliyor
        }

        public async Task DeleteAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync(); // Veritabanından siliniyor
            }
        }
        public async Task<IEnumerable<Team>> GetAllTeamsAsync() // Yeni metot
        {
            return await _context.Teams.ToListAsync(); // Sadece takımları döndürür
        }
    }
}
