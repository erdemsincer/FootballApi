using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Domain.Entities
{
    public class Player
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? BirthCountry { get; set; }
        public string? Nationality { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public int? Number { get; set; }
        public string? Position { get; set; }
        public string? Photo { get; set; }

        // Takım ilişkisi
        public int? TeamId { get; set; }
        public Team? Team { get; set; }

    }
}
