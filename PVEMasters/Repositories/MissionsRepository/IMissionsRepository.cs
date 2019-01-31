using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Repositories.MissionsRepository
{
    public interface IMissionsRepository
    {

        IEnumerable<Mission> getAllAvailableMissions();

        IEnumerable<Mission> getAllInProgressMissions();

        IEnumerable<Mission> getAllCompletedMissions();

        void StartMission(Mission mission);
    }
}
