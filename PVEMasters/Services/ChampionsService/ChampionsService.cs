using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.ApiModels;
using PVEMasters.ModelMappers;
using PVEMasters.Models;
using PVEMasters.Services.ChampionsRepository;
using Microsoft.EntityFrameworkCore;

namespace PVEMasters.Services.ChampionsService
{
    public class ChampionsService : IChampionsService
    {
        private IChampionsRepository _championsRepository;

        public ChampionsService(IChampionsRepository championsRepository)
        {
            _championsRepository = championsRepository;
        }

        public int AddChampion(ChampionsOwned champ)
        {
            return _championsRepository.AddChampion(champ);
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
            var championsTask = await _championsRepository.getAvailableChampionsForAccount();
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
    }
}
