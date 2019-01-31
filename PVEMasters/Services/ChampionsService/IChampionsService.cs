using PVEMasters.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Services.ChampionsService
{
    public interface IChampionsService
    {
        IEnumerable<ApiChampions> getChampions();

        ApiChampions getChampion(int champId);
    }
}
