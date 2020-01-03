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

        public int AddEquipment(EquipmentOwned equipmentOwned)
        {
            _context.Add(equipmentOwned);
            _context.SaveChanges();

            return equipmentOwned.Id;
        }

        public async Task<string> BuyEquipmentForAccount(EquipmentOwned equip)
        {
            var equipmentOwnedId = AddEquipment(equip);
            return "Equipment added to your collection";
        }

        public async Task<IEnumerable<EquipmentOwned>> getAccountEquipment(string userName)
        {
            return await _context.EquipmentOwned.Include(equipment => equipment.Equipment).ThenInclude(equipment => equipment.EquipmentStats).ThenInclude(equipmentStat => equipmentStat.Stat).Where(equipment => equipment.AccountUserName.Equals(userName)).ToListAsync();
        }

        public async Task<IEnumerable<Equipment>> getAllEquipmentsAvailableForAccount(String userName)
        {
            return await _context.Equipment
                                 .Where(equipment => !_context.EquipmentOwned
                                                              .Where(equipmentOwned => equipmentOwned.AccountUserName == userName)
                                                              .Any(equipmentOwned => equipmentOwned.EquipmentId == equipment.Id)
                                       ).Include(equipment => equipment.EquipmentStats).ThenInclude(equipmentStat => equipmentStat.Stat).OrderBy(a => a.Name).ToListAsync();
        }

        public Equipment getEquipment(int equipmentId)
        {
            return _context.Equipment.Where(equipment => equipment.Id == equipmentId).FirstOrDefault();
        }

        public async Task<Equipment> getEquipmentByName(string name)
        {
            return await _context.Equipment.Where(equipment => equipment.Name == name).FirstOrDefaultAsync();
        }
    }
}
