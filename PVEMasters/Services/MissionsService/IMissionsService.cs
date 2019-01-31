using PVEMasters.ApiModels;
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Services.MissionsService
{
    public interface IMissionsService
    {
        IEnumerable<ApiMission> getAllAvailableMissions();

        IEnumerable<ApiMission> getAllInProgressMissions();

        IEnumerable<ApiMission> getAllCompletedMissions();

        void StartMission(ApiMission mission);
    }
}
