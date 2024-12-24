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
    public class StadiumRepository: IStadiumRepository
    {
        private readonly AppDbContext _context;

        public StadiumRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stadium>> GetAllAsync()
        {
            return await _context.Stadiums.ToListAsync();
        }

        public async Task<Stadium> GetByIdAsync(int id)
        {
            return await _context.Stadiums.FindAsync(id);
        }

        public async Task AddAsync(Stadium stadium)
        {
            await _context.Stadiums.AddAsync(stadium);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stadium stadium)
        {
            _context.Stadiums.Update(stadium);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var stadium = await _context.Stadiums.FindAsync(id);
            if (stadium != null)
            {
                _context.Stadiums.Remove(stadium);
                await _context.SaveChangesAsync();
            }
        }
    }
}
