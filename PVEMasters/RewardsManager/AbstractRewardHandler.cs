using PVEMasters.IRewardsManager;
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.RewardsManager
{
    public abstract class AbstractRewardHandler: IRewardHandler
    {
        private IRewardHandler _nextHandler;

        public IRewardHandler SetNext(IRewardHandler handler)
        {
            this._nextHandler = handler;

            return handler;
        }

        public virtual void Handle(MissionRwards missionReward, ApplicationUser user)
        {
            if (this._nextHandler != null)
            {
                this._nextHandler.Handle(missionReward, user);
            }
        }
    }
}
