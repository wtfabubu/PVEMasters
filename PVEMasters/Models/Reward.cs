using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PVEMasters.Models
{
    public partial class Reward
    {
        public Reward()
        {
            MissionRwards = new HashSet<MissionRwards>();
        }

        [Key]
        public string Name { get; set; }
        public string Details { get; set; }

        public ICollection<MissionRwards> MissionRwards { get; set; }
    }
}
