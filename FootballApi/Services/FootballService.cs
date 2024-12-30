using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces.Football;
using FootballApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using System.Numerics;
using System.Text.Json;

namespace FootballApi.Services
{
    public class FootballService : IPlayerService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _dbContext;

        public FootballService(HttpClient httpClient, AppDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayersFromApiAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/players/profiles"),
                Headers =
                {
                    { "x-rapidapi-key", "2c7eb57010mshe769a372d7fa526p193665jsne8ceff271973" },
                    { "x-rapidapi-host", "api-football-v1.p.rapidapi.com" },
                }
            };

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(jsonResponse);

                return jsonDocument.RootElement
                    .GetProperty("response")
                    .EnumerateArray()
                    .Select(playerJson =>
                    {
                        var player = playerJson.GetProperty("player");

                        return new PlayerDto
                        {
                           
                            Name = player.GetProperty("name").GetString(),
                            FirstName = player.GetProperty("firstname").GetString(),
                            LastName = player.GetProperty("lastname").GetString(),
                            Age = player.TryGetProperty("age", out var age) && age.ValueKind == JsonValueKind.Number
                                ? (int?)age.GetInt32()
                                : null,
                            BirthDate = player.TryGetProperty("birth", out var birth) &&
                                        birth.TryGetProperty("date", out var birthDate) && birthDate.ValueKind == JsonValueKind.String
                                ? birthDate.GetString()
                                : null,
                            BirthPlace = player.TryGetProperty("birth", out var birthPlace) &&
                                         birthPlace.TryGetProperty("place", out var place) && place.ValueKind == JsonValueKind.String
                                ? place.GetString()
                                : null,
                            BirthCountry = player.TryGetProperty("birth", out var birthCountry) &&
                                           birthCountry.TryGetProperty("country", out var country) && country.ValueKind == JsonValueKind.String
                                ? country.GetString()
                                : null,
                            Nationality = player.TryGetProperty("nationality", out var nationality) && nationality.ValueKind == JsonValueKind.String
                                ? nationality.GetString()
                                : null,
                            Height = player.TryGetProperty("height", out var height) && height.ValueKind == JsonValueKind.String
                                ? height.GetString()
                                : null,
                            Weight = player.TryGetProperty("weight", out var weight) && weight.ValueKind == JsonValueKind.String
                                ? weight.GetString()
                                : null,
                            Number = player.TryGetProperty("number", out var number) && number.ValueKind == JsonValueKind.Number
                                ? (int?)number.GetInt32()
                                : null,
                            Position = player.TryGetProperty("position", out var position) && position.ValueKind == JsonValueKind.String
                                ? position.GetString()
                                : null,
                            Photo = player.TryGetProperty("photo", out var photo) && photo.ValueKind == JsonValueKind.String
                                ? photo.GetString()
                                : null
                        };
                    }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("API'den veri alınırken bir hata oluştu.", ex);
            }
        }

        public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
        {
            try
            {
                // Veritabanındaki oyuncuları DTO'ya dönüştürüp döndür
                return await _dbContext.Players
                    .Select(player => new PlayerDto
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
                        Photo = player.Photo,
                        TeamId = player.TeamId,
                        

                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Veritabanından veri alınırken bir hata oluştu.", ex);
            }
        }
        public async Task AddPlayerAsync(PlayerDto playerDto)
        {
            try
            {
                // PlayerDto'yu Player entity'sine dönüştür
                var playerEntity = new Player
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
                    Photo = playerDto.Photo,
                    TeamId = playerDto.TeamId
                };

                // Veritabanına ekle
                await _dbContext.Players.AddAsync(playerEntity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Oyuncu eklenirken bir hata oluştu.", ex);
            }
        }
        public async Task DeletePlayerByIdAsync(int id)
        {
            var player = await _dbContext.Players.FindAsync(id); // FindAsync ile direkt Id eşleşmesini kontrol eder
            if (player == null)
                throw new Exception($"ID {id} ile eşleşen bir oyuncu bulunamadı.");

            _dbContext.Players.Remove(player);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePlayerAsync(PlayerDto playerDto)
        {
            var player = await _dbContext.Players.FindAsync(playerDto.Id);
            if (player == null)
                throw new Exception($"ID {playerDto.Id} ile eşleşen bir oyuncu bulunamadı.");

            player.Name = playerDto.Name;
            player.FirstName = playerDto.FirstName;
            player.LastName = playerDto.LastName;
            player.Age = playerDto.Age;
            player.BirthDate = playerDto.BirthDate;
            player.BirthPlace = playerDto.BirthPlace;
            player.BirthCountry = playerDto.BirthCountry;
            player.Nationality = playerDto.Nationality;
            player.Height = playerDto.Height;
            player.Weight = playerDto.Weight;
            player.Number = playerDto.Number;
            player.Position = playerDto.Position;
            player.Photo = playerDto.Photo;
            player.TeamId = playerDto.TeamId;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<PlayerDto> GetPlayerByIdAsync(int id)
        {
            var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.Id == id);
            if (player == null)
                throw new Exception("Oyuncu bulunamadı.");

            return new PlayerDto
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
                Photo = player.Photo,
                TeamId = player.TeamId,
                
            };
        }
        public async Task TransferPlayerAsync(int playerId, int newTeamId)
        {
            var player = await _dbContext.Players.FindAsync(playerId);
            if (player == null)
            {
                throw new Exception("Oyuncu bulunamadı.");
            }

            var newTeam = await _dbContext.Teams.FindAsync(newTeamId);
            if (newTeam == null)
            {
                throw new Exception("Yeni takım bulunamadı.");
            }

            // Oyuncunun takımını değiştiriyoruz
            player.TeamId = newTeamId;

            _dbContext.Players.Update(player);
            await _dbContext.SaveChangesAsync();
        }







    }
}