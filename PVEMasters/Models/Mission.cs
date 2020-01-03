using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class Mission
    {
        public Mission()
        {
            MissionRwards = new HashSet<MissionRwards>();
            MissionsForAccount = new HashSet<MissionsForAccount>();
        }

        public int Id { get; set; }
        public int? AverageLvlRequired { get; set; }
        public string Name { get; set; }
        public int? AccountLvlRequired { get; set; }

        public ICollection<MissionRwards> MissionRwards { get; set; }
        public ICollection<MissionsForAccount> MissionsForAccount { get; set; }
    }
}
