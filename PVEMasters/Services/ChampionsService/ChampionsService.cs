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

        public async Task<string> BuyChampionForUser(ApiChampions champion, string userName)
        {
            var account = await _accountRepository.getUserByUsername(userName);
            if(account.AccountStatistics.Gold < champion.Cost)
            {
                return "Insufficient gold!";
            }
            Champions champ = await GetChampionFromDB(champion);
            ChampionsOwned champOwned = CreateChampionForAccount(userName, champ);
            await _championsRepository.BuyChampionForAccount(champOwned);
            return "Character successfully added to your collection!";
        }

        public async Task<IEnumerable<ApiChampionsOwned>> getAccountChampions(String userName)
        {
            var championsTask = await _championsRepository.getAccountChampions(userName);
            List<ChampionsOwned> champions = championsTask.ToList();
            List<ApiChampionsOwned> championsToReturn = new List<ApiChampionsOwned>();

            champions.ForEach(champ => championsToReturn.Add(ChampionsOwnedMapper.convertToApiModel(champ)));
            return championsToReturn;
        }

        public async Task<IEnumerable<ApiChampions>> getAvailableChampionsForAccount(string userName)
        {


            var championsTask = await _championsRepository.getAvailableChampionsForAccount(userName);
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

        private static ChampionsOwned CreateChampionForAccount(string userName, Champions champ)
        {
            return new ChampionsOwned
            {
                AccountUsername = userName,
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
