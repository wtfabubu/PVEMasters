using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Models
{
    public partial class PvPSafeList
    {
        public int Id { get; set; }
        public string AttackerId { get; set; }
        public string DeffenderId { get; set; }
    }
}
