using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class ChampionsOwned
    {
        public int Id { get; set; }
        public string AccountUsername { get; set; }
        public int ChampionsId { get; set; }
        public int Lvl { get; set; }
        public int Experience { get; set; }
        public int Health { get; set; }
        public int Agility { get; set; }
        public int Strength { get; set; }
        public int MagicPower { get; set; }
        public bool Equipped { get; set; }

        public Champions Champions { get; set; }
    }
}
