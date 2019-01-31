using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.Models;

namespace PVEMasters.Repositories.EquipmentRepository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private UserDbContext _context;

        public EquipmentRepository(UserDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Equipment> getAllEquipments()
        {
            return _context.Equipment.OrderBy(a => a.Name).ToList();
        }

        public Equipment getEquipment(int equipmentId)
        {
            return _context.Equipment.Where(equipment => equipment.Id == equipmentId).FirstOrDefault();
        }
    }
}
