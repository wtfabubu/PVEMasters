using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class MissionsForAccount
    {
        public int Id { get; set; }
        public string AccountUsername { get; set; }
        public int MissionId { get; set; }
        public int StatusId { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }

        public Mission Mission { get; set; }
        public MissionStatus Status { get; set; }
    }
}
