using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Application.Dtos
{
    public class ComparePlayersDto
    {
        public PlayerDto Player1 { get; set; }
        public PlayerDto Player2 { get; set; }
    }
}
