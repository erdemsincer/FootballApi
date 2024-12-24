using FootballApi.Application.Interfaces;
using FootballApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Persistence.Repositories
{
    public class LeagueRepository: ILeagueRepository
    {
        private readonly AppDbContext _context;

        public LeagueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<League>> GetAllAsync()
        {
            return await _context.Leagues.ToListAsync();
        }

        public async Task<League> GetByIdAsync(int id)
        {
            return await _context.Leagues.FindAsync(id);
        }

        public async Task AddAsync(League league)
        {
            await _context.Leagues.AddAsync(league);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(League league)
        {
            _context.Leagues.Update(league);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var league = await _context.Leagues.FindAsync(id);
            if (league != null)
            {
                _context.Leagues.Remove(league);
                await _context.SaveChangesAsync();
            }
        }
    }
}
