using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.Models;
using PVEMasters.Repositories.AccountRepository;

namespace PVEMasters.Services.AccountService
{
    public class AccountService : IAccountService
    {

        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void AddMissionRewardsToAccount(List<MissionRwards> missionRewards)
        {

                ApplicationUser usr = _accountRepository.getUserByUsername("Test3@abv.bg");

                foreach (var missionReward in missionRewards)
                {
                    if (missionReward.RewardName == "Experience")
                    {
                        usr.AccountStatistics.Experience += missionReward.Amount;
                        if (usr.AccountStatistics.Experience > usr.AccountStatistics.Lvl * 1000)
                        {
                            usr.AccountStatistics.Experience -= usr.AccountStatistics.Lvl * 1000;
                            usr.AccountStatistics.Lvl++;
                        }
                    }
                    else if (missionReward.RewardName == "Gold")
                    {
                        usr.AccountStatistics.Gold += missionReward.Amount;
                    }
                    else
                    {

                    }
                }
                _accountRepository.UpdateUser(usr);
        }
    }
}
