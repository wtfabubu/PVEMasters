using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PVEMasters.Repositories.MissionsRepository
{
    public interface IMissionsRepository
    {

        Task<IEnumerable<Mission>> getAllAvailableMissions(String userId);

        Task<IEnumerable<MissionsForAccount>> GetAllMissionsWithGivenStatus(String userId, String status);

        Task StartMission(Mission mission, String userId);

        Task CompleteMission(int missionId);
    }
}
