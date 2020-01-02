using PVEMasters.ApiModels;
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PVEMasters.Services.MissionsService
{
    public interface IMissionsService
    {
        Task<IEnumerable<ApiMission>> getAllAvailableMissions();

        IEnumerable<ApiMission> getAllInProgressMissions();

        IEnumerable<ApiMission> getAllCompletedMissions();

        Task StartMission(ApiMission mission, String userName);
    }
}
