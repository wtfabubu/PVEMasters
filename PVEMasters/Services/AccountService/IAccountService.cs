using PVEMasters.ApiModels;
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
        Task<ApiProfile> GetUserProfileByUserName(String userName);
        Task<ICollection<ApiProfile>> GetAvailablePVPAccounts(string userName);
        string GetAccountIdByUserName(string username);
        Task<string> AttackOpponent(ApiProfile opponent, string userName);
    }
}
