using PVEMasters.ApiModels;
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ModelMappers
{
    public class ChampionsMapper
    {
        public static ApiChampions convertToApiModel(Champions Model)
        {
            return new ApiChampions { Name = Model.Name, Avatar = Model.Avatar, Cost = Model.Cost, Icon = Model.Icon, Story = Model.Story };
        }

        public static Champions converToDbModel(ApiChampions ApiModel)
        {
            return new Champions { Name = ApiModel.Name, Avatar = ApiModel.Avatar, Cost = ApiModel.Cost, Icon = ApiModel.Icon, Story = ApiModel.Story };
        }
    }
}
