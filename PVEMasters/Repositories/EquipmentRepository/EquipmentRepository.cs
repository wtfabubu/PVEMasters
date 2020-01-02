using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.Models;
using Microsoft.EntityFrameworkCore;

namespace PVEMasters.Repositories.EquipmentRepository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private UserDbContext _context;

        public EquipmentRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Equipment>> getAllEquipments()
        {
            return await _context.Equipment.OrderBy(a => a.Name).ToListAsync();
        }

        public Equipment getEquipment(int equipmentId)
        {
            return _context.Equipment.Where(equipment => equipment.Id == equipmentId).FirstOrDefault();
        }
    }
}
