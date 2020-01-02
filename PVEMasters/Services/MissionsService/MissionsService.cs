using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.ApiModels;
using PVEMasters.ModelMappers;
using PVEMasters.Models;
using PVEMasters.Repositories.MissionsRepository;
using PVEMasters.Services.AccountService;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public async Task<IEnumerable<ApiMission>> getAllAvailableMissions()
        {
            var missionsTask = await _missionsRepository.getAllAvailableMissions();
            List<Mission> missions = missionsTask.ToList();
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

        public async Task StartMission(ApiMission mission, String userName)
        {
            var miss = MissionsMapper.converToDbModel(mission);
            
            await _accountService.AddMissionRewardsToAccount(miss.MissionRwards.ToList(), userName);
            await _missionsRepository.StartMission(miss, userName);
        }
    }
}
