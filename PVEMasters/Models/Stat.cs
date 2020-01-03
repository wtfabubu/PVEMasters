using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class Stat
    {
        public Stat()
        {
            ChampionOwnedStats = new HashSet<ChampionOwnedStats>();
            ChampionsStats = new HashSet<ChampionsStats>();
            EquipmentStats = new HashSet<EquipmentStats>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ChampionOwnedStats> ChampionOwnedStats { get; set; }
        public ICollection<ChampionsStats> ChampionsStats { get; set; }
        public ICollection<EquipmentStats> EquipmentStats { get; set; }
    }
}
