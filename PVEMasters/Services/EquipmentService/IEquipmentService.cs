using PVEMasters.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Services.EquipmentService
{
    public interface IEquipmentService
    {
        Task<IEnumerable<ApiEquipment>> getAllEquipmentsAvailableForAccount(String userName);

        ApiEquipment getEquipment(int champId);
        Task<String> BuyEquipmentForUser(ApiEquipment equipment, string userName);
        Task<IEnumerable<ApiEquipment>> getAccountEquipments(string userName);
    }
}
