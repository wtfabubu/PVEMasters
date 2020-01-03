using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class Champions
    {
        public Champions()
        {
            ChampionsOwned = new HashSet<ChampionsOwned>();
            ChampionsStats = new HashSet<ChampionsStats>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Story { get; set; }
        public string Avatar { get; set; }
        public string Icon { get; set; }
        public int Cost { get; set; }

        public ICollection<ChampionsOwned> ChampionsOwned { get; set; }
        public ICollection<ChampionsStats> ChampionsStats { get; set; }
    }
}
