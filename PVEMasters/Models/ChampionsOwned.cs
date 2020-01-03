using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class ChampionsOwned
    {
        public ChampionsOwned()
        {
            ChampionOwnedStats = new HashSet<ChampionOwnedStats>();
        }

        public int Id { get; set; }
        public string AccountUsername { get; set; }
        public int ChampionsId { get; set; }
        public int Lvl { get; set; }
        public int Experience { get; set; }
        public bool Equipped { get; set; }

        public Champions Champions { get; set; }
        public ICollection<ChampionOwnedStats> ChampionOwnedStats { get; set; }
    }
}
