using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.ApiModels;
using PVEMasters.ModelMappers;
using PVEMasters.Models;
using PVEMasters.Services.ChampionsRepository;
using Microsoft.EntityFrameworkCore;
using PVEMasters.Repositories.AccountRepository;

namespace PVEMasters.Services.ChampionsService
{
    public class ChampionsService : IChampionsService
    {
        private IChampionsRepository _championsRepository;
        private IAccountRepository _accountRepository;

        public ChampionsService(IChampionsRepository championsRepository, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _championsRepository = championsRepository;
        }

        public int AddChampion(ChampionsOwned champ)
        {
            return _championsRepository.AddChampion(champ);
        }

        public async Task<string> BuyChampionForUser(ApiChampions champion, string userId)
        {
            var account = await _accountRepository.getUserByUsername(userId);
            if (account.AccountStatistics.Gold < champion.Cost)
            {
                return "Insufficient gold!";
            }
            Champions champ = await GetChampionFromDB(champion);
            ChampionsOwned champOwned = CreateChampionForAccount(userId, champ);
            await _championsRepository.BuyChampionForAccount(champOwned);
            account.AccountStatistics.Gold -= champion.Cost;
            await _accountRepository.UpdateAccount();
            return "Character successfully added to your collection!";
        }

        public async Task<IEnumerable<ApiChampionsOwned>> getAccountChampions(String userId)
        {
            var championsTask = await _championsRepository.getAccountChampions(userId);
            List<ChampionsOwned> champions = championsTask.ToList();
            List<ApiChampionsOwned> championsToReturn = new List<ApiChampionsOwned>();

            champions.ForEach(champ => championsToReturn.Add(ChampionsOwnedMapper.convertToApiModel(champ)));
            return championsToReturn;
        }

        public async Task<IEnumerable<ApiChampions>> getAvailableChampionsForAccount(string userId)
        {
            var championsTask = await _championsRepository.getAvailableChampionsForAccount(userId);
            List<Champions> champions = championsTask.ToList();
            List<ApiChampions> championsToReturn = new List<ApiChampions>();

            champions.ForEach(champ => championsToReturn.Add(ChampionsMapper.convertToApiModel(champ)));
            return championsToReturn;
        }

        public ApiChampions getChampion(int champId)
        {
            Champions champion = _championsRepository.getChampion(champId);

            return ChampionsMapper.convertToApiModel(champion);
        }

        public async Task<IEnumerable<ApiChampions>> getChampions()
        {
            var championsTask = await _championsRepository.getChampions();
            List<Champions> champions = championsTask.ToList();
            List<ApiChampions> championsToReturn = new List<ApiChampions>();

            champions.ForEach(champ => championsToReturn.Add(ChampionsMapper.convertToApiModel(champ)));
            return championsToReturn;
        }

        public async Task<string> EquipChampion(string userName, ApiChampionsOwned champion)
        {
            string result = "You can have a maximum of 3 champions equipped";
            var equippedChampions = await GetNumberOfEquippedChampionsForAccount(userName);
            ChampionsOwned champ = await GetChampionOwnedFromDB(champion, userName);
            if (equippedChampions < 3)
            {
                champ.Equipped = true;
                result = await _championsRepository.EquipChampion(champ);
            }
            return result;
        }

        public async Task<string> UnequipChampion(string userName, ApiChampionsOwned champion)
        {
            ChampionsOwned champ = await GetChampionOwnedFromDB(champion, userName);
            champ.Equipped = false;
            return await _championsRepository.UnequipChampion(champ);
        }

        public async Task<int> GetNumberOfEquippedChampionsForAccount(string userName)
        {
            return await _championsRepository.GetNumberOfEquippedChampionsForAccount(userName);
        }

        private async Task<ChampionsOwned> GetChampionOwnedFromDB(ApiChampionsOwned champion, string userName)
        {
            return await _championsRepository.GetChampionOwnedFromDB(champion.Champions.Name, userName);
        }

        private ChampionsOwned CreateChampionForAccount(string userName, Champions champ)
        {
            return new ChampionsOwned
            {
                AccountId = _accountRepository.GetAccountIdByUserName(userName),
                ChampionsId = champ.Id,
                Equipped = false,
                Experience = 0,
                Lvl = 1,
            };
        }

        private async Task<Champions> GetChampionFromDB(ApiChampions champion)
        {
            return await _championsRepository.getChampionByName(champion.Name);
        }
    }
}
