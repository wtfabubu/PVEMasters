﻿using System;
using System.Collections.Generic;

namespace PVEMasters.Models
{
    public partial class Reward
    {
        public Reward()
        {
            MissionRwards = new HashSet<MissionRwards>();
        }

        public string Name { get; set; }
        public string Details { get; set; }

        public ICollection<MissionRwards> MissionRwards { get; set; }
    }
}
