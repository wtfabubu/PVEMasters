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

        Task<IEnumerable<Mission>> getAllAvailableMissions();

        IEnumerable<Mission> getAllInProgressMissions();

        IEnumerable<Mission> getAllCompletedMissions();

        Task StartMission(Mission mission, String userName);
    }
}
