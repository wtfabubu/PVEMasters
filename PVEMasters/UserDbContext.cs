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
        public virtual DbSet<ChampionsStats> ChampionsStats { get; set; }
        public virtual DbSet<EquipmentStats> EquipmentStats { get; set; }
        public virtual DbSet<Stat> Stat { get; set; }
        public virtual DbSet<EquipmentOwned> EquipmentOwned { get; set; }
        public virtual DbSet<ChampionOwnedStats> ChampionOwnedStats { get; set; }

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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ChampionOwnedStats>(entity =>
            {
                entity.HasKey(e => new { e.ChampionsOwnedId, e.StatId });

                entity.HasOne(d => d.ChampionsOwned)
                    .WithMany(p => p.ChampionOwnedStats)
                    .HasForeignKey(d => d.ChampionsOwnedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChampionOwnedStats_ChampionsOwned");

                entity.HasOne(d => d.Stat)
                    .WithMany(p => p.ChampionOwnedStats)
                    .HasForeignKey(d => d.StatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChampionOwnedStats_Stat");
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
                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Champions)
                    .WithMany(p => p.ChampionsOwned)
                    .HasForeignKey(d => d.ChampionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChampionsOwned_Champions");
            });

            modelBuilder.Entity<ChampionsStats>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Champion)
                    .WithMany(p => p.ChampionsStats)
                    .HasForeignKey(d => d.ChampionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChampionsStats_Champions");

                entity.HasOne(d => d.Stat)
                    .WithMany(p => p.ChampionsStats)
                    .HasForeignKey(d => d.StatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChampionsStats_Stat");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EquipmentOwned>(entity =>
            {
                entity.Property(e => e.AccountUserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentOwned)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentOwned_Equipment");
            });

            modelBuilder.Entity<EquipmentStats>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentStats)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentStats_Equipment");

                entity.HasOne(d => d.Stat)
                    .WithMany(p => p.EquipmentStats)
                    .HasForeignKey(d => d.StatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentStats_Stat");
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

            modelBuilder.Entity<Stat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
