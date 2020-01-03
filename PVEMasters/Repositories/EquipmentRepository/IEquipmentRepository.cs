using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Repositories.EquipmentRepository
{
    public interface IEquipmentRepository
    {
        Equipment getEquipment(int equipmentId);

        Task<IEnumerable<Equipment>> getAllEquipmentsAvailableForAccount(String userName);
        Task<string> BuyEquipmentForAccount(EquipmentOwned equip);
        Task<Equipment> getEquipmentByName(string name);
        Task<IEnumerable<EquipmentOwned>> getAccountEquipment(string userName);
    }
}
