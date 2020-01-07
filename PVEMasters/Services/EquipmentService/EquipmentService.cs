using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.ApiModels;
using PVEMasters.ModelMappers;
using PVEMasters.Models;
using PVEMasters.Repositories.AccountRepository;
using PVEMasters.Repositories.EquipmentRepository;

namespace PVEMasters.Services.EquipmentService
{
    public class EquipmentService : IEquipmentService
    {
        private IEquipmentRepository _equipmentRepository;
        private IAccountRepository _accountRepository;

        public EquipmentService(IEquipmentRepository equipmentRepository, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _equipmentRepository = equipmentRepository;
        }

        public async Task<string> BuyEquipmentForUser(ApiEquipment equipment, string userName)
        {
            var account = await _accountRepository.getUserByUsername(userName);
            if (account.AccountStatistics.Gold < equipment.Cost)
            {
                return "Insufficient gold!";
            }
            Equipment equip = await GetEquipmentFromDB(equipment);
            EquipmentOwned equipOwned = CreateEquipmentForAccount(userName, equip);
            await _equipmentRepository.BuyEquipmentForAccount(equipOwned);
            account.AccountStatistics.Gold -= equipment.Cost;
            await _accountRepository.UpdateAccount();
            return "Equipment successfully added to your collection!";
        }

        public async Task<IEnumerable<ApiEquipment>> getAllEquipmentsAvailableForAccount(String userName)
        {
            var championsTask = await _equipmentRepository.getAllEquipmentsAvailableForAccount(userName);
            List<Equipment> champions = championsTask.ToList();
            List <ApiEquipment> championsToReturn = new List<ApiEquipment>();

            champions.ForEach(equipment => championsToReturn.Add(EquipmentMapper.convertToApiModel(equipment)));
            return championsToReturn;
        }

        public ApiEquipment getEquipment(int champId)
        {
            Equipment champion = _equipmentRepository.getEquipment(champId);

            return EquipmentMapper.convertToApiModel(champion);
        }

        private static EquipmentOwned CreateEquipmentForAccount(string userName, Equipment equip)
        {
            return new EquipmentOwned
            {
                AccountUserName = userName,
                EquipmentId = equip.Id
            };
        }

        private async Task<Equipment> GetEquipmentFromDB(ApiEquipment equipment)
        {
            return await _equipmentRepository.getEquipmentByName(equipment.Name);
        }

        public async Task<IEnumerable<ApiEquipment>> getAccountEquipments(string userName)
        {
            var championsTask = await _equipmentRepository.getAccountEquipment(userName);
            List<EquipmentOwned> equipments = championsTask.ToList();
            List<ApiEquipment> equipmentToReturn = new List<ApiEquipment>();
            equipments.ForEach(eq => equipmentToReturn.Add(EquipmentOwnedMapper.convertToApiModel(eq)));
            return equipmentToReturn;
        }
    }
}
