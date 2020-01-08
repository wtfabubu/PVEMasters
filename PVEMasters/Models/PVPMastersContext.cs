using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PVEMasters.Models
{
    public partial class PVPMastersContext : DbContext
    {
        public PVPMastersContext()
        {
        }

        public PVPMastersContext(DbContextOptions<PVPMastersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PvPSafeList> PvPsafeList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=PVPMasters;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PvPSafeList>(entity =>
            {
                entity.ToTable("PvPSafeList");


                entity.Property(e => e.AttackerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DeffenderId)
                    .IsRequired()
                    .HasMaxLength(450);
            });
        }
    }
}
