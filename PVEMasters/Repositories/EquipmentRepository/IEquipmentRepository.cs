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

        IEnumerable<Equipment> getAllEquipments();
    }
}
