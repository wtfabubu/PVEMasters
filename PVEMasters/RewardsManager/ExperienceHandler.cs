using PVEMasters.Models;
using PVEMasters.Repositories.AccountRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.RewardsManager
{
    public class ExperienceHandler : AbstractRewardHandler
    {

        public override void Handle(MissionRwards missionReward, ApplicationUser user)
        {

            if (missionReward.RewardName == "Experience")
            {
                user.AccountStatistics.Experience += missionReward.Amount;
                if (user.AccountStatistics.Experience >= user.AccountStatistics.Lvl * 1000)
                {
                    user.AccountStatistics.Experience -= user.AccountStatistics.Lvl * 1000;
                    user.AccountStatistics.Lvl++;
                }
            }
            else
            {
                base.Handle(missionReward, user);
            }
        }
    }
}
