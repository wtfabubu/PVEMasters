using PVEMasters.ApiModels;
using PVEMasters.Models;
using System.Collections.Generic;

namespace PVEMasters.ModelMappers
{
    public class EquipmentOwnedMapper
    {
        public static ApiEquipment convertToApiModel(EquipmentOwned Model)
        {
            return new ApiEquipment { Name = Model.Equipment.Name, Avatar = Model.Equipment.Avatar, Cost = Model.Equipment.Cost, Icon = Model.Equipment.Icon, Description = Model.Equipment.Description, EquipmentStats = CreateEuqipmentStats(Model.Equipment.EquipmentStats) };
        }

        public static EquipmentOwned converToDbModel(ApiEquipment ApiModel)
        {
            return new EquipmentOwned { Equipment = new Equipment { Name = ApiModel.Name, Avatar = ApiModel.Avatar, Cost = ApiModel.Cost, Icon = ApiModel.Icon, Description = ApiModel.Description} };
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
