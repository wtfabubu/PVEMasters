using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ChampionsOwned = new HashSet<ChampionsOwned>();
        }

        public string Gender { get; set; }
        public string InGameName { get; set; }
        public int AccountStatisticsId { get; set; }

        public AccountStatistic AccountStatistics { get; set; }
        public ICollection<ChampionsOwned> ChampionsOwned { get; set; }
    }
}
