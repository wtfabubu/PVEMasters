using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.ApiModels;
using PVEMasters.ModelMappers;
using PVEMasters.Models;
using PVEMasters.Repositories.EquipmentRepository;

namespace PVEMasters.Services.EquipmentService
{
    public class EquipmentService : IEquipmentService
    {
        private IEquipmentRepository _equipmentRepository;

        public EquipmentService(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public IEnumerable<ApiEquipment> getAllEquipments()
        {
            List<Equipment> champions = _equipmentRepository.getAllEquipments().ToList();
            List<ApiEquipment> championsToReturn = new List<ApiEquipment>();

            champions.ForEach(equipment => championsToReturn.Add(EquipmentMapper.convertToApiModel(equipment)));
            return championsToReturn;
        }

        public ApiEquipment getEquipment(int champId)
        {
            Equipment champion = _equipmentRepository.getEquipment(champId);

            return EquipmentMapper.convertToApiModel(champion);
        }
    }
}
