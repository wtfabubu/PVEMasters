using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.IRewardsManager
{
    public interface IRewardHandler
    {
        IRewardHandler SetNext(IRewardHandler handler);

        void Handle(MissionRwards missionReward, ApplicationUser user);

    }
}
