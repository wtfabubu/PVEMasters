
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.RewardsManager
{
    public class ChampionExperienceHandler : AbstractRewardHandler
    {
        public override void Handle(MissionRwards missionReward, ApplicationUser user)
        {

            if (missionReward.RewardName == "Champion Experience")
            {

                List<ChampionsOwned> championsEligibleForReward = user.ChampionsOwned.Where(champ => champ.Equipped).ToList();
                foreach (var champion in championsEligibleForReward)
                {
                    champion.Experience += missionReward.Amount;
                    if (champion.Experience >= champion.Lvl * 100)
                    {
                        champion.Experience -= champion.Lvl * 100;
                        champion.Lvl++;
                        champion.ChampionOwnedStats.ToList().ForEach(stat => stat.Amount += stat.Stat.Name != "Health" ? Convert.ToInt32(champion.Lvl * 0.25) : Convert.ToInt32(champion.Lvl * 25));
                    }
                }
            }
            else
            {
                base.Handle(missionReward, user);
            }
        }
    }
}
