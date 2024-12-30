
using FootballApi.Application.Dtos;
using FootballApi.Domain.Entities;

namespace FootballApi.WebUI1.ViewModels
{
    public class CompareViewModel
    {

        public PlayerDto Player1 { get; set; }
        public PlayerDto Player2 { get; set; }
        public PlayerStatistic Player1Stats { get; set; }
        public PlayerStatistic Player2Stats { get; set; }
    }
}
