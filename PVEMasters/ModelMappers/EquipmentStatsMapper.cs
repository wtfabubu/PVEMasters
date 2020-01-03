using PVEMasters.ApiModels;
using PVEMasters.Models;

namespace PVEMasters.ModelMappers
{
    public class EquipmentStatsMapper
    {
        public static ApiStats convertToApiModel(EquipmentStats Model)
        {
            return new ApiStats { Amount = Model.Amount, Name = Model.Stat.Name };
        }
    }
}
