using PVEMasters.ApiModels;
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ModelMappers
{
    public class ChampionsOwnedMapper
    {
        public static ApiChampionsOwned convertToApiModel(ChampionsOwned Model)
        {
            return new ApiChampionsOwned
            {
                ChampionStats = CreateChampionStats(Model),
                Equipped = Model.Equipped,
                Experience = Model.Experience,
                Lvl = Model.Lvl,
                Champions = new ApiChampions
                {
                    Name = Model.Champions.Name,
                    Avatar = Model.Champions.Avatar,
                    Cost = Model.Champions.Cost,
                    Icon = Model.Champions.Icon,
                    Story = Model.Champions.Story
                }
            };
        }

        public static ChampionsOwned converToDbModel(ApiChampionsOwned ApiModel)
        {
            return new ChampionsOwned
            {
                //TODO:
                //Agility = ApiModel.Agility,
                Equipped = ApiModel.Equipped,
                Experience = ApiModel.Experience,
                //Health = ApiModel.Health,
                Lvl = ApiModel.Lvl,
                //MagicPower = ApiModel.MagicPower,
                //Strength = ApiModel.Strength,
                Champions = new Champions
                {
                    Name = ApiModel.Champions.Name,
                    Avatar = ApiModel.Champions.Avatar,
                    Cost = ApiModel.Champions.Cost,
                    Icon = ApiModel.Champions.Icon,
                    Story = ApiModel.Champions.Story
                }
            };
        }

        private static List<ApiStats> CreateChampionStats(ChampionsOwned stats)
        {
            List<ApiStats> equipmentStatsToReturn = new List<ApiStats>();

            foreach (var equipmentStat in stats.ChampionOwnedStats)
            {
                equipmentStatsToReturn.Add(ChampionOwnedStatMapper.convertToApiModel(equipmentStat));
            }
            return equipmentStatsToReturn;
        }
    }
}
