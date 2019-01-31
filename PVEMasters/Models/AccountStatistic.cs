using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class AccountStatistic
    {
        public AccountStatistic()
        {
        }

        public int Id { get; set; }
        public int? ChampionsOwned { get; set; }
        public int? AchievementsCompleted { get; set; }
        public int Gold { get; set; }
        public int Lvl { get; set; }
        public int Experience { get; set; }
    }
}
