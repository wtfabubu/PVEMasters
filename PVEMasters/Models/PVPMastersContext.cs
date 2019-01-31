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

        public virtual DbSet<AccountStatistic> AccountStatistic { get; set; }
        public virtual DbSet<Champions> Champions { get; set; }
        public virtual DbSet<ChampionsOwned> ChampionsOwned { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Mission> Mission { get; set; }
        public virtual DbSet<MissionRwards> MissionRwards { get; set; }
        public virtual DbSet<MissionsForAccount> MissionsForAccount { get; set; }
        public virtual DbSet<MissionStatus> MissionStatus { get; set; }
        public virtual DbSet<Reward> Reward { get; set; }

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
            modelBuilder.Entity<AccountStatistic>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Gold).HasMaxLength(10);
            });

            modelBuilder.Entity<Champions>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Story).IsRequired();
            });

            modelBuilder.Entity<ChampionsOwned>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountUsername)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Champions)
                    .WithMany(p => p.ChampionsOwned)
                    .HasForeignKey(d => d.ChampionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChampionsOwned_Champions");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Mission>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<MissionRwards>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RewardName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.MissionRwards)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MissionRwards_Mission");

                entity.HasOne(d => d.RewardNameNavigation)
                    .WithMany(p => p.MissionRwards)
                    .HasForeignKey(d => d.RewardName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MissionRwards_Reward");
            });

            modelBuilder.Entity<MissionsForAccount>(entity =>
            {
                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

                entity.Property(e => e.AccountUsername)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.MissionsForAccount)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MissionsForAccount_Mission");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.MissionsForAccount)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_MissionsForAccount_MissionStatus");
            });

            modelBuilder.Entity<MissionStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Details).IsRequired();
            });
        }
    }
}
