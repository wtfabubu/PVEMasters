using PVEMasters.ApiModels;
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Services.ChampionsService
{
    public interface IChampionsService
    {
        Task<IEnumerable<ApiChampions>> getChampions();

        ApiChampions getChampion(int champId);
        int AddChampion(ChampionsOwned champ1);
        Task<IEnumerable<ApiChampionsOwned>> getAccountChampions(String userName);
        Task<IEnumerable<ApiChampions>> getAvailableChampionsForAccount(String userName);
        Task<String> BuyChampionForUser(ApiChampions champion, string userName);
    }
}
