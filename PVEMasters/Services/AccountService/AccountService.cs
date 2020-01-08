using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.ApiModels;
using PVEMasters.ModelMappers;
using PVEMasters.Models;
using PVEMasters.Repositories.AccountRepository;
using PVEMasters.RewardsManager;
using PVEMasters.Services.ChampionsRepository;

namespace PVEMasters.Services.AccountService
{
    public class AccountService : IAccountService
    {

        private IAccountRepository _accountRepository;
        private IChampionsRepository _championsRepository;

        public AccountService(IAccountRepository accountRepository, IChampionsRepository championsRepository)
        {
            _championsRepository = championsRepository;
            _accountRepository = accountRepository;
        }

        public async Task AddMissionRewardsToAccount(List<MissionRwards> missionRewards, String userName)
        {
            ApplicationUser usr = await _accountRepository.getUserByUsername(userName);
            RewardsClient.AddRewards(RewardsChain.GetChain(), missionRewards, usr);
            await _accountRepository.UpdateUser(usr);
        }

        public async Task<string> AttackOpponent(ApiProfile opponent, string userName)
        {
            string result = "You must equip 3 champions before attacking an opponent!";
            var countChampEquipped = await _championsRepository.GetNumberOfEquippedChampionsForAccount(userName);
            if(countChampEquipped > 2)
            {
                result = await BattleLogic(opponent, userName);
                await _accountRepository.AddAccountToSafeList(opponent.UserName, userName);
                await UpdateAccountAchivements(userName, result);
            }
            return result;
        }


        public int CreateAccountStatistic(AccountStatistic accountStatistic)
        {
            return _accountRepository.CreateAccountStatistic(accountStatistic);
        }

        public string GetAccountIdByUserName(string username)
        {
            return _accountRepository.GetAccountIdByUserName(username);
        }

        public async Task<ICollection<ApiProfile>> GetAvailablePVPAccounts(string userName)
        {
            var profiles = await _accountRepository.GetAvailablePVPAccounts(userName);
            ICollection<ApiProfile> profilesToReturn = new List<ApiProfile>();
            profiles.ToList().ForEach(profile => profilesToReturn.Add(new ApiProfile { UserName = profile.UserName, AccountStatistics = profile.AccountStatistics, ChampionsOwned = CreateChampionsForProfile(profile, true) }));
            return profilesToReturn;
        }

        public async Task<ApiProfile> GetUserProfileByUserName(String userName)
        {
            var profile = await _accountRepository.getUserByUsername(userName);
            ICollection<ApiChampionsOwned> championsForProfile = CreateChampionsForProfile(profile, false);
            ApiProfile profileToReturn = new ApiProfile { UserName = profile.UserName, AccountStatistics = profile.AccountStatistics, ChampionsOwned = championsForProfile };
            return profileToReturn;
        }

        private ICollection<ApiChampionsOwned> CreateChampionsForProfile(ApplicationUser profile, bool onlyEquipped)
        {
            ICollection<ApiChampionsOwned> championsForProfile = new List<ApiChampionsOwned>();
            if (onlyEquipped)
            {
                foreach(var champion in profile.ChampionsOwned.ToList())
                {
                    if(champion.Equipped)
                    {
                        championsForProfile.Add(ChampionsOwnedMapper.convertToApiModel(champion));
                    }
                }
            } else
            {
                profile.ChampionsOwned.ToList().ForEach(champ => championsForProfile.Add(ChampionsOwnedMapper.convertToApiModel(champ)));
            }
            
            return championsForProfile;
        }

        private async Task<string> BattleLogic(ApiProfile opponent, string userName)
        {
            string result = "Defeat";
            int opponentStrSum = 0;
            int opponentHealthSum = 0;
            int opponentAgiSum = 0;
            int opponentMPSum = 0;
            opponent.ChampionsOwned.Where(champ => champ.Equipped).ToList().ForEach(champ => opponentStrSum += champ.ChampionStats.First(element => element.Name == "Strength").Amount);
            opponent.ChampionsOwned.Where(champ => champ.Equipped).ToList().ForEach(champ => opponentHealthSum += champ.ChampionStats.First(element => element.Name == "Health").Amount);
            opponent.ChampionsOwned.Where(champ => champ.Equipped).ToList().ForEach(champ => opponentAgiSum += champ.ChampionStats.First(element => element.Name == "Agility").Amount);
            opponent.ChampionsOwned.Where(champ => champ.Equipped).ToList().ForEach(champ => opponentMPSum += champ.ChampionStats.First(element => element.Name == "MagicPower").Amount);

            var attacker = await _accountRepository.getUserByUsername(userName);
            int attackerStrSum = 0;
            int attackerHealthSum = 0;
            int attackerAgiSum = 0;
            int attackerMPSum = 0;
            attacker.ChampionsOwned.Where(champ => champ.Equipped).ToList().ForEach(champ => attackerStrSum += champ.ChampionOwnedStats.First(element => element.Stat.Name == "Strength").Amount);
            attacker.ChampionsOwned.Where(champ => champ.Equipped).ToList().ForEach(champ => attackerHealthSum += champ.ChampionOwnedStats.First(element => element.Stat.Name == "Health").Amount);
            attacker.ChampionsOwned.Where(champ => champ.Equipped).ToList().ForEach(champ => attackerAgiSum += champ.ChampionOwnedStats.First(element => element.Stat.Name == "Agility").Amount);
            attacker.ChampionsOwned.Where(champ => champ.Equipped).ToList().ForEach(champ => attackerMPSum += champ.ChampionOwnedStats.First(element => element.Stat.Name == "MagicPower").Amount);
            var opponentTurnsToKillAttacker = attackerHealthSum / (opponentStrSum * 0.3 + opponentAgiSum * 0.24 + opponentMPSum * 0.26);
            var attackerTurnsToKillOpponent = opponentHealthSum / (attackerStrSum * 0.3 + attackerAgiSum * 0.24 + attackerMPSum * 0.26);
            if (opponentTurnsToKillAttacker > attackerTurnsToKillOpponent)
            {

                result = "Victory";
            }

            return result;
        }

        private async Task UpdateAccountAchivements(string userName, string result)
        {
            var account = await _accountRepository.getUserByUsername(userName);
            if (result == "Victory")
            {
                account.AccountStatistics.AchievementsCompleted += 3;
            }
            else
            {
                account.AccountStatistics.AchievementsCompleted -= 2;
            }
            await _accountRepository.UpdateAccount();
        }
    }
}
