using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.ApiModels;
using PVEMasters.ModelMappers;
using PVEMasters.Models;
using PVEMasters.Repositories.MissionsRepository;
using PVEMasters.Services.AccountService;

namespace PVEMasters.Services.MissionsService
{
    public class MissionsService : IMissionsService
    {
        private IMissionsRepository _missionsRepository;
        private IAccountService _accountService;

        public MissionsService(IMissionsRepository missionsRepository, IAccountService accountService)
        {
            _missionsRepository = missionsRepository;
            _accountService = accountService;
        }

        public IEnumerable<ApiMission> getAllAvailableMissions()
        {
            List<Mission> missions = _missionsRepository.getAllAvailableMissions().ToList();
            List<ApiMission> missionsToReturn = new List<ApiMission>();

            missions.ForEach(champ => missionsToReturn.Add(MissionsMapper.convertToApiModel(champ)));
            return missionsToReturn;
        }

        public IEnumerable<ApiMission> getAllCompletedMissions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApiMission> getAllInProgressMissions()
        {
            throw new NotImplementedException();
        }

        public void StartMission(ApiMission mission)
        {
            var miss = MissionsMapper.converToDbModel(mission);
            
            _accountService.AddMissionRewardsToAccount(miss.MissionRwards.ToList());
            _missionsRepository.StartMission(miss);
        }
    }
}
