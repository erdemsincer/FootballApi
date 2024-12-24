using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Domain.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Stadium { get; set; }
        public string Coach { get; set; }

        // Oyuncular ile ilişki
        public ICollection<Player> Players { get; set; }
    }
}
