using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ApiModels
{
    public class ApiChampionsOwned
    {
        public string AccountUsername { get; set; }
        public int ChampionsId { get; set; }
        public int Lvl { get; set; }
        public int Experience { get; set; }
        public int Health { get; set; }
        public int Agility { get; set; }
        public int Strength { get; set; }
        public int MagicPower { get; set; }
        public bool Equipped { get; set; }

        public ApiChampions Champions { get; set; }
    }
}
