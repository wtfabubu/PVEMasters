using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Champions> Champions { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Mission> Mission { get; set; }
        public virtual DbSet<Reward> Reward { get; set; }
        public virtual DbSet<MissionRwards> MissionRwards { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<MissionStatus> MissionStatus { get; set; }
        public virtual DbSet<MissionsForAccount> MissionsForAccount { get; set; }
        public virtual DbSet<AccountStatistic> AccountStatistic { get; set; }
        public virtual DbSet<ChampionsOwned> ChampionsOwned { get; set; }
    }
}
