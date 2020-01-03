using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class EquipmentStats
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int StatId { get; set; }
        public int Amount { get; set; }

        public Equipment Equipment { get; set; }
        public Stat Stat { get; set; }
    }
}
