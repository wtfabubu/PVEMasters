using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ApiModels
{
    public class ApiMissionRewards
    {
        public string RewardName { get; set; }
        public int MissionId { get; set; }
        public int Amount { get; set; }
    }
}
