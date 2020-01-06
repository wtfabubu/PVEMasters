using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ApiModels
{
    public class ApiProfile
    {
        public string UserName { get; set; }
        public AccountStatistic AccountStatistics { get; set; }
        public ICollection<ApiChampionsOwned> ChampionsOwned { get; set; }
    }
}
