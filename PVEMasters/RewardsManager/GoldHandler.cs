using PVEMasters.Models;
using PVEMasters.Repositories.AccountRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.RewardsManager
{
    public class GoldHandler: AbstractRewardHandler
    {

        public override void Handle(MissionRwards missionReward, ApplicationUser user)
        {

            if (missionReward.RewardName == "Gold")
            {
                user.AccountStatistics.Gold += missionReward.Amount;
            }
            else
            {
                base.Handle(missionReward, user);
            }
        }
    }
}
