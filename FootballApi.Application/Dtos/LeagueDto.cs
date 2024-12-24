using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Application.Dtos
{
    public class LeagueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string LogoUrl { get; set; }
        public int NumberOfTeams { get; set; }
    }
}
