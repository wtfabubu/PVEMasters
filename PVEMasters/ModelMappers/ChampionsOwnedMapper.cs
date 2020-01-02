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
                Agility = Model.Agility,
                Equipped = Model.Equipped,
                Experience = Model.Experience,
                Health = Model.Health,
                Lvl = Model.Lvl,
                MagicPower = Model.MagicPower,
                Strength = Model.Strength,
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

                Agility = ApiModel.Agility,
                Equipped = ApiModel.Equipped,
                Experience = ApiModel.Experience,
                Health = ApiModel.Health,
                Lvl = ApiModel.Lvl,
                MagicPower = ApiModel.MagicPower,
                Strength = ApiModel.Strength,
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
    }
}
