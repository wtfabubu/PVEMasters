using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.ApiModels;
using PVEMasters.ModelMappers;
using PVEMasters.Models;
using PVEMasters.Services.ChampionsRepository;

namespace PVEMasters.Services.ChampionsService
{
    public class ChampionsService : IChampionsService
    {
        private IChampionsRepository _championsRepository;

        public ChampionsService(IChampionsRepository championsRepository)
        {
            _championsRepository = championsRepository;
        }

        public ApiChampions getChampion(int champId)
        {
            Champions champion = _championsRepository.getChampion(champId);

            return ChampionsMapper.convertToApiModel(champion);
        }

        public IEnumerable<ApiChampions> getChampions()
        {
            List<Champions> champions = _championsRepository.getChampions().ToList();
            List<ApiChampions> championsToReturn = new List<ApiChampions>(); 

            champions.ForEach(champ => championsToReturn.Add(ChampionsMapper.convertToApiModel(champ)));
            return championsToReturn;
        }
    }
}
