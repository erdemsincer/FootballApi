using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces;
using FootballApi.Domain.Entities;

namespace FootballApi.Services
{
    public class StadiumService: IStadiumService
    {
        private readonly IStadiumRepository _stadiumRepository;

        public StadiumService(IStadiumRepository stadiumRepository)
        {
            _stadiumRepository = stadiumRepository;
        }

        public async Task<IEnumerable<StadiumDto>> GetAllAsync()
        {
            var stadiums = await _stadiumRepository.GetAllAsync();
            return stadiums.Select(s => new StadiumDto
            {
                Id = s.Id,
                Name = s.Name,
                Location = s.Location,
                Capacity = s.Capacity,
                PhotoUrl = s.PhotoUrl
            });
        }

        public async Task<StadiumDto> GetByIdAsync(int id)
        {
            var stadium = await _stadiumRepository.GetByIdAsync(id);
            if (stadium == null) return null;

            return new StadiumDto
            {
                Id = stadium.Id,
                Name = stadium.Name,
                Location = stadium.Location,
                Capacity = stadium.Capacity,
                PhotoUrl = stadium.PhotoUrl
            };
        }

        public async Task AddAsync(StadiumDto stadiumDto)
        {
            var stadium = new Stadium
            {
                Name = stadiumDto.Name,
                Location = stadiumDto.Location,
                Capacity = stadiumDto.Capacity,
                PhotoUrl = stadiumDto.PhotoUrl
            };

            await _stadiumRepository.AddAsync(stadium);
        }

        public async Task UpdateAsync(StadiumDto stadiumDto)
        {
            var stadium = new Stadium
            {
                Id = stadiumDto.Id,
                Name = stadiumDto.Name,
                Location = stadiumDto.Location,
                Capacity = stadiumDto.Capacity,
                PhotoUrl = stadiumDto.PhotoUrl
            };

            await _stadiumRepository.UpdateAsync(stadium);
        }

        public async Task DeleteAsync(int id)
        {
            await _stadiumRepository.DeleteAsync(id);
        }
    }
}
