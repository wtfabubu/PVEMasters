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
        public bool Equipped { get; set; }

        public ICollection<ApiStats> ChampionStats { get; set; }
        public ApiChampions Champions { get; set; }
    }
}
