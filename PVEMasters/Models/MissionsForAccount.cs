using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PVEMasters.Models
{
    public partial class MissionsForAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AccountUsername { get; set; }
        public int MissionId { get; set; }
        public int? StatusId { get; set; }

        public Mission Mission { get; set; }
        public MissionStatus Status { get; set; }
    }
}
