using PVEMasters.ApiModels;
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ModelMappers
{
    public class EquipmentMapper
    {
        public static ApiEquipment convertToApiModel(Equipment Model)
        {
            return new ApiEquipment { Name = Model.Name, Avatar = Model.Avatar, Cost = Model.Cost, Icon = Model.Icon, Description = Model.Description, EquipmentStats = CreateEuqipmentStats(Model.EquipmentStats) };
        }

        public static Equipment converToDbModel(ApiEquipment ApiModel)
        {
            return new Equipment { Name = ApiModel.Name, Avatar = ApiModel.Avatar, Cost = ApiModel.Cost, Icon = ApiModel.Icon, Description = ApiModel.Description };
        }

        private static List<ApiStats> CreateEuqipmentStats(ICollection<EquipmentStats> equipmentStats)
        {
            List<ApiStats> equipmentStatsToReturn = new List<ApiStats>();

            foreach (var equipmentStat in equipmentStats)
            {
                equipmentStatsToReturn.Add(EquipmentStatsMapper.convertToApiModel(equipmentStat));
            }
            return equipmentStatsToReturn;
        }
    }
}
