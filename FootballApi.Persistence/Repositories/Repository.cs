using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces;
using FootballApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballApi.Persistence.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPlayersAsync(IEnumerable<PlayerDto> players)
        {
            var playerEntities = players.Select(playerDto => new Player
            {
                Name = playerDto.Name,
                FirstName = playerDto.FirstName,
                LastName = playerDto.LastName,
                Age = playerDto.Age,
                BirthDate = playerDto.BirthDate,
                BirthPlace = playerDto.BirthPlace,
                BirthCountry = playerDto.BirthCountry,
                Nationality = playerDto.Nationality,
                Height = playerDto.Height,
                Weight = playerDto.Weight,
                Number = playerDto.Number,
                Position = playerDto.Position,
                Photo = playerDto.Photo
            });

            await _context.Players.AddRangeAsync(playerEntities);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
        {
            return await _context.Players.Select(player => new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Age = player.Age,
                BirthDate = player.BirthDate,
                BirthPlace = player.BirthPlace,
                BirthCountry = player.BirthCountry,
                Nationality = player.Nationality,
                Height = player.Height,
                Weight = player.Weight,
                Number = player.Number,
                Position = player.Position,
                Photo = player.Photo
            }).ToListAsync();
        }
    }
}
