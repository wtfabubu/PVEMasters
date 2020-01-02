using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.RewardsManager
{
    public class RewardsClient
    {
        public static void AddRewards(AbstractRewardHandler handler, List<MissionRwards> missionRewards, ApplicationUser user)
        {
            foreach (var reward in missionRewards)
            {
                handler.Handle(reward, user);
            }
        }
    }
}
