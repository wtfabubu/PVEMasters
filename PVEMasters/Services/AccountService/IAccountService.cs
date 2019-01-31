using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Services.AccountService
{
    public interface IAccountService
    {
        void AddMissionRewardsToAccount(List<MissionRwards> missionRewards);
    }
}
