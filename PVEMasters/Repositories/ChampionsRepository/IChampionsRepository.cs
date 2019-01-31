using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Services.ChampionsRepository
{
    public interface IChampionsRepository
    {
        IEnumerable<Champions> getChampions();

        Champions getChampion(int champId);
    }
}
