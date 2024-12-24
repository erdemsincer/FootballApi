using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Domain.Entities
{
    public class PlayerStatistic
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int MatchesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int KeyPasses { get; set; }
        public double PassAccuracy { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }

    }
}
