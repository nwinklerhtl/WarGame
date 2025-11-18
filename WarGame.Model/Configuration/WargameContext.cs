using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WarGame.Model.Models;

namespace WarGame.Model.Configuration;

public partial class WargameContext : DbContext
{
    public WargameContext(DbContextOptions<WargameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Faction> Factions { get; set; }

    public virtual DbSet<Tank> Tanks { get; set; }

    public virtual DbSet<TankStat> TankStats { get; set; }

    public virtual DbSet<TankType> TankTypes { get; set; }

    public virtual DbSet<TankWeapon> TankWeapons { get; set; }

    public virtual DbSet<Weapon> Weapons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("country");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Continent)
                .HasMaxLength(50)
                .HasColumnName("continent");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Faction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("faction");

            entity.HasIndex(e => e.CountryId, "country_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Ideology)
                .HasMaxLength(100)
                .HasColumnName("ideology");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.Factions)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("faction_ibfk_1");
        });

        modelBuilder.Entity<Tank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tank");

            entity.HasIndex(e => e.CountryId, "country_id");

            entity.HasIndex(e => e.FactionId, "faction_id");

            entity.HasIndex(e => e.TankTypeId, "tank_type_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.FactionId).HasColumnName("faction_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TankTypeId).HasColumnName("tank_type_id");
            entity.Property(e => e.YearIntroduced).HasColumnName("year_introduced");

            entity.HasOne(d => d.Country).WithMany(p => p.Tanks)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("tank_ibfk_1");

            entity.HasOne(d => d.Faction).WithMany(p => p.Tanks)
                .HasForeignKey(d => d.FactionId)
                .HasConstraintName("tank_ibfk_2");

            entity.HasOne(d => d.TankType).WithMany(p => p.Tanks)
                .HasForeignKey(d => d.TankTypeId)
                .HasConstraintName("tank_ibfk_3");
        });

        modelBuilder.Entity<TankStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tank_stat");

            entity.HasIndex(e => e.TankId, "tank_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArmorThickness).HasColumnName("armor_thickness");
            entity.Property(e => e.EnginePower).HasColumnName("engine_power");
            entity.Property(e => e.TankId).HasColumnName("tank_id");
            entity.Property(e => e.TopSpeed).HasColumnName("top_speed");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Tank).WithMany(p => p.TankStats)
                .HasForeignKey(d => d.TankId)
                .HasConstraintName("tank_stat_ibfk_1");
        });

        modelBuilder.Entity<TankType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tank_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TankWeapon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tank_weapon");

            entity.HasIndex(e => e.TankId, "tank_id");

            entity.HasIndex(e => e.WeaponId, "weapon_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmmoCount).HasColumnName("ammo_count");
            entity.Property(e => e.MountPosition)
                .HasMaxLength(50)
                .HasColumnName("mount_position");
            entity.Property(e => e.TankId).HasColumnName("tank_id");
            entity.Property(e => e.WeaponId).HasColumnName("weapon_id");

            entity.HasOne(d => d.Tank).WithMany(p => p.TankWeapons)
                .HasForeignKey(d => d.TankId)
                .HasConstraintName("tank_weapon_ibfk_1");

            entity.HasOne(d => d.Weapon).WithMany(p => p.TankWeapons)
                .HasForeignKey(d => d.WeaponId)
                .HasConstraintName("tank_weapon_ibfk_2");
        });

        modelBuilder.Entity<Weapon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("weapon");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaliberMm).HasColumnName("caliber_mm");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.RateOfFire).HasColumnName("rate_of_fire");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
