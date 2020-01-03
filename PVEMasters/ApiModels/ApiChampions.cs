using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ApiModels
{
    public class ApiChampions
    {
        public string Name { get; set; }
        public string Story { get; set; }
        public string Avatar { get; set; }
        public string Icon { get; set; }
        public int Cost { get; set; }

        public ICollection<ApiStats> ChampionStats { get; set; }
    }
}
