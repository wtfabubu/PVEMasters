using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Services.ChampionsRepository
{
    public interface IChampionsRepository
    {
        Task<IEnumerable<Champions>> getChampions();

        Champions getChampion(int champId);
        int AddChampion(ChampionsOwned champ);
        Task<IEnumerable<ChampionsOwned>> getAccountChampions(String userName);
        Task<IEnumerable<Champions>> getAvailableChampionsForAccount();
    }
}
