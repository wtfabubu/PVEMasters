using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Repositories.AccountRepository
{
    public interface IAccountRepository
    {
        void AddMissionRewardsToAccount(List<MissionRwards> missionRewards);
        ApplicationUser getUserByUsername(string userName);
        void UpdateUser(ApplicationUser usr);
    }
}
