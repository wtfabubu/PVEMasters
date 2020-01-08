using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class EquipmentOwned
    {
        public string AccountId { get; set; }
        public int EquipmentId { get; set; }

        public ApplicationUser Account { get; set; }
        public Equipment Equipment { get; set; }
    }
}
