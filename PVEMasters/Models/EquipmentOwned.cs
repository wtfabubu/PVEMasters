using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class EquipmentOwned
    {
        public int Id { get; set; }
        public string AccountUserName { get; set; }
        public int EquipmentId { get; set; }

        public Equipment Equipment { get; set; }
    }
}
