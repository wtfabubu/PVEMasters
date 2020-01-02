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
        public int? ChampionsOwned { get; set; } = 3;
        public int? AchievementsCompleted { get; set; } = 0;
        public int Gold { get; set; } = 0;
        public int Lvl { get; set; } = 1;
        public int Experience { get; set; } = 0;
    }
}
