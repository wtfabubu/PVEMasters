using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ApiModels
{
    public class ApiMission
    {
        public int Id { get; set; }
        public int AverageLvlRequired { get; set; }
        public string Name { get; set; }
        public int AccountLvlRequired { get; set; }

        public ICollection<ApiMissionRewards> MissionRewards { get; set; }
        //public ICollection<MissionsForAccount> MissionsForAccount { get; set; }
    }
}
