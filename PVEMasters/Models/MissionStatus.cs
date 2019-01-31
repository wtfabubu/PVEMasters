using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class MissionStatus
    {
        public MissionStatus()
        {
            MissionsForAccount = new HashSet<MissionsForAccount>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public ICollection<MissionsForAccount> MissionsForAccount { get; set; }
    }
}
