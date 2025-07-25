﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NaftalanHotelSystem.Persistence.DataAccessLayer;

#nullable disable

namespace NaftalanHotelSystem.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250626144446_AddPackageEntity")]
    partial class AddPackageEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.EquipmentTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.ToTable("EquipmentTranslations");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<short>("DurationDay")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.PackageTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.ToTable("PackageTranslations");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<short>("Area")
                        .HasColumnType("smallint");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("Member")
                        .HasColumnType("smallint");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("YoutubeVideoLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.RoomEquipment", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.HasKey("RoomId", "EquipmentId");

                    b.HasIndex("EquipmentId");

                    b.ToTable("RoomEquipments");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.RoomTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomTranslations");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.TreatmentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("TreatmentMethods");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.TreatmentMethodTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TreatmentMethodId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TreatmentMethodId");

                    b.ToTable("TreatmentMethodTranslations");
                });

            modelBuilder.Entity("PackageTreatmentMethod", b =>
                {
                    b.Property<int>("PackagesId")
                        .HasColumnType("int");

                    b.Property<int>("TreatmentMethodsId")
                        .HasColumnType("int");

                    b.HasKey("PackagesId", "TreatmentMethodsId");

                    b.HasIndex("TreatmentMethodsId");

                    b.ToTable("PackageTreatmentMethod");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.EquipmentTranslation", b =>
                {
                    b.HasOne("NaftalanHotelSystem.Domain.Entites.Equipment", "Equipment")
                        .WithMany("EquipmentTranslations")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.PackageTranslation", b =>
                {
                    b.HasOne("NaftalanHotelSystem.Domain.Entites.Package", "Packages")
                        .WithMany("PackageTranslations")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Packages");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.RoomEquipment", b =>
                {
                    b.HasOne("NaftalanHotelSystem.Domain.Entites.Equipment", "Equipment")
                        .WithMany("RoomEquipments")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NaftalanHotelSystem.Domain.Entites.Room", "Room")
                        .WithMany("Equipments")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.RoomTranslation", b =>
                {
                    b.HasOne("NaftalanHotelSystem.Domain.Entites.Room", "Room")
                        .WithMany("RoomTranslations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.TreatmentMethodTranslation", b =>
                {
                    b.HasOne("NaftalanHotelSystem.Domain.Entites.TreatmentMethod", "TreatmentMethod")
                        .WithMany("Translations")
                        .HasForeignKey("TreatmentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TreatmentMethod");
                });

            modelBuilder.Entity("PackageTreatmentMethod", b =>
                {
                    b.HasOne("NaftalanHotelSystem.Domain.Entites.Package", null)
                        .WithMany()
                        .HasForeignKey("PackagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NaftalanHotelSystem.Domain.Entites.TreatmentMethod", null)
                        .WithMany()
                        .HasForeignKey("TreatmentMethodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.Equipment", b =>
                {
                    b.Navigation("EquipmentTranslations");

                    b.Navigation("RoomEquipments");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.Package", b =>
                {
                    b.Navigation("PackageTranslations");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.Room", b =>
                {
                    b.Navigation("Equipments");

                    b.Navigation("RoomTranslations");
                });

            modelBuilder.Entity("NaftalanHotelSystem.Domain.Entites.TreatmentMethod", b =>
                {
                    b.Navigation("Translations");
                });
#pragma warning restore 612, 618
        }
    }
}
