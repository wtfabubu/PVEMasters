using Microsoft.AspNetCore.Identity;

namespace PVEMasters.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Gender { get; set; }
        public string InGameName { get; set; }
        public int AccountStatisticsId { get; set; }

        public AccountStatistic AccountStatistics { get; set; }
    }
}
