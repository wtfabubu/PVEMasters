using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class ChampionOwnedStats
    {
        public int ChampionsOwnedId { get; set; }
        public int StatId { get; set; }
        public int Amount { get; set; }

        public ChampionsOwned ChampionsOwned { get; set; }
        public Stat Stat { get; set; }
    }
}
