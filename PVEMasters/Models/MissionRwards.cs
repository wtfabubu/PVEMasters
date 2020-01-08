using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class MissionRwards
    {
        public string RewardName { get; set; }
        public int MissionId { get; set; }
        public int Amount { get; set; }

        public Mission Mission { get; set; }
        public Reward RewardNameNavigation { get; set; }
    }
}
