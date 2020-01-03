using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class ChampionsStats
    {
        public int Id { get; set; }
        public int ChampionId { get; set; }
        public int StatId { get; set; }
        public int Amount { get; set; }

        public Champions Champion { get; set; }
        public Stat Stat { get; set; }
    }
}
