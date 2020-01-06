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

        public async Task<IEnumerable<ApiMission>> getAllAvailableMissions(String userName)
        {
            var missionsTask = await _missionsRepository.getAllAvailableMissions(userName);
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
            
            await _missionsRepository.StartMission(miss, userName);
        }

        public async Task CompleteMission(ApiMission mission, String userName)
        {
            var miss = MissionsMapper.converToDbModel(mission);

            await _accountService.AddMissionRewardsToAccount(miss.MissionRwards.ToList(), userName);
            await _missionsRepository.CompleteMission(mission.MissionForAccountId);
        }

        public async Task<IEnumerable<ApiMission>> GetAllMissionsWithGivenStatus(string userName, string status)
        {
            var missions = await _missionsRepository.GetAllMissionsWithGivenStatus(userName, status);
            List<ApiMission> missionsToReturn = new List<ApiMission>();
            missions.ToList().ForEach(misson => missionsToReturn.Add(CreateMission(misson)));
            return missionsToReturn;
        }

        private ApiMission CreateMission(MissionsForAccount misson)
        {
            var ApiMisson = MissionsMapper.convertToApiModel(misson.Mission);
            ApiMisson.MissionForAccountId = misson.Id;
            return ApiMisson;
        }
    }
}
