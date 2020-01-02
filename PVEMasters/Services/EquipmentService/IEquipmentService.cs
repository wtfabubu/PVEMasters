using PVEMasters.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Services.EquipmentService
{
    public interface IEquipmentService
    {
        Task<IEnumerable<ApiEquipment>> getAllEquipments();

        ApiEquipment getEquipment(int champId);
    }
}
