using PVEMasters.ApiModels;
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ModelMappers
{
    public class ChampionOwnedStatMapper
    {
        public static ApiStats convertToApiModel(ChampionOwnedStats Model)
        {
            return new ApiStats { Amount = Model.Amount, Name = Model.Stat.Name };
        }
    }
}
