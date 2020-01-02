using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Services.AccountService
{
    public interface IAccountService
    {
        Task AddMissionRewardsToAccount(List<MissionRwards> missionRewards, String userName);
        int CreateAccountStatistic(AccountStatistic accountStatistic);
        Task<ApplicationUser> GetUserProfileByUserName(String userName);
    }
}
