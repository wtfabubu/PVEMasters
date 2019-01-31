using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string Icon { get; set; }
        public int Cost { get; set; }
    }
}
