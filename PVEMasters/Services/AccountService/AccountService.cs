using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.Models;
using PVEMasters.Repositories.AccountRepository;
using PVEMasters.RewardsManager;

namespace PVEMasters.Services.AccountService
{
    public class AccountService : IAccountService
    {

        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task AddMissionRewardsToAccount(List<MissionRwards> missionRewards, String userName)
        {
            ApplicationUser usr = await _accountRepository.getUserByUsername(userName);
            RewardsClient.AddRewards(RewardsChain.GetChain(), missionRewards, usr);
            await _accountRepository.UpdateUser(usr);
        }

        public int CreateAccountStatistic(AccountStatistic accountStatistic)
        {
            return _accountRepository.CreateAccountStatistic(accountStatistic);
        }

        public async Task<ApplicationUser> GetUserProfileByUserName(String userName)
        {
            return await _accountRepository.getUserByUsername(userName);
        }
    }
}
