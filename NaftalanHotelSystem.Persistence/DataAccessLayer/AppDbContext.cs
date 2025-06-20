﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Persistence.Configurations.Common;

namespace NaftalanHotelSystem.Persistence.DataAccessLayer;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<RoomEquipment> RoomEquipments { get; set; }
    public DbSet<RoomTranslation> RoomTranslations { get; set; }
    public DbSet<EquipmentTranslation> EquipmentTranslations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
   
}
