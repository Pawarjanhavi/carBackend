﻿// <auto-generated />
using System;
using Car2Go.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Car2Go.Migrations
{
    [DbContext(typeof(Car2GoDBContext))]
    [Migration("20241211141446_CreateDatabase")]
    partial class CreateDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Car2Go.Models.AgentCar", b =>
                {
                    b.Property<int>("AgentCarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AgentCarId"));

                    b.Property<int>("AgentId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("AvailableDate")
                        .HasColumnType("date");

                    b.Property<bool>("AvailableStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Colour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int");

                    b.Property<int>("year")
                        .HasColumnType("int");

                    b.HasKey("AgentCarId");

                    b.HasIndex("LocationId");

                    b.ToTable("AgentCars");
                });

            modelBuilder.Entity("Car2Go.Models.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarId"));

                    b.Property<DateOnly>("AvailableDate")
                        .HasColumnType("date");

                    b.Property<bool>("AvailableStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Colour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int");

                    b.Property<int>("year")
                        .HasColumnType("int");

                    b.HasKey("CarId");

                    b.HasIndex("LocationId");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            CarId = 1,
                            AvailableDate = new DateOnly(2024, 12, 11),
                            AvailableStatus = true,
                            Colour = "White",
                            ImageUrl = "https://res.cloudinary.com/dhnatfkvb/image/upload/v1733810275/oyqzglkehqgbutmggfas.jpg",
                            LicensePlate = "MP09CP7235",
                            LocationId = 2,
                            Make = "Honda",
                            Model = "Honda City",
                            PricePerDay = 3000m,
                            TotalSeats = 5,
                            year = 2020
                        },
                        new
                        {
                            CarId = 2,
                            AvailableDate = new DateOnly(2024, 12, 12),
                            AvailableStatus = true,
                            Colour = "Red",
                            ImageUrl = "https://res.cloudinary.com/dhnatfkvb/image/upload/v1733810445/honda-amaze-2018-red_vruzbl.jpg",
                            LicensePlate = "MH50PS2184",
                            LocationId = 1,
                            Make = "Honda",
                            Model = "Honda Amaze",
                            PricePerDay = 3200m,
                            TotalSeats = 5,
                            year = 2018
                        },
                        new
                        {
                            CarId = 3,
                            AvailableDate = new DateOnly(2024, 12, 13),
                            AvailableStatus = true,
                            Colour = "Black",
                            ImageUrl = "http://res.cloudinary.com/dhnatfkvb/image/upload/v1733813312/o6xkah1rdvc4lytcydhp.webp",
                            LicensePlate = "MP09DY9472",
                            LocationId = 3,
                            Make = "Maruti Suzuki ",
                            Model = " Maruti Suzuki Swift",
                            PricePerDay = 2000m,
                            TotalSeats = 5,
                            year = 2021
                        });
                });

            modelBuilder.Entity("Car2Go.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            LocationId = 1,
                            Address = "123,Honda Showroom",
                            City = "Mumbai",
                            Country = "India",
                            State = "Maharshtra",
                            ZipCode = "411912"
                        },
                        new
                        {
                            LocationId = 2,
                            Address = "711,Honda Showroom",
                            City = "Indore",
                            Country = "India",
                            State = "Madhya Pradesh",
                            ZipCode = "452014"
                        },
                        new
                        {
                            LocationId = 3,
                            Address = "Indore",
                            City = "Indore",
                            Country = "India",
                            State = "Madhya Pradesh",
                            ZipCode = "452014"
                        });
                });

            modelBuilder.Entity("Car2Go.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Car2Go.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Car2Go.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("DropOffDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("PickUpDate")
                        .HasColumnType("date");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("CarId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Car2Go.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<DateOnly>("ReviewCreatedAt")
                        .HasColumnType("date");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("CarId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Car2Go.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("RoleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "zahabiya@gmail.com",
                            FirstName = "Zahabiya",
                            IsDeleted = false,
                            LastName = "Kapadia",
                            Password = "zahabiya@12",
                            PhoneNumber = "9128347653",
                            RoleType = "[\"Admin\"]"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "tanya@gmail.com",
                            FirstName = "Tanya",
                            IsDeleted = false,
                            LastName = "Patel",
                            Password = "tanya@12",
                            PhoneNumber = "9827342651",
                            RoleType = "[\"User\"]"
                        },
                        new
                        {
                            UserId = 3,
                            Email = "rahul@gmail.com",
                            FirstName = "Rahul",
                            IsDeleted = false,
                            LastName = "Gupta",
                            Password = "rahul@123",
                            PhoneNumber = "8837624393",
                            RoleType = "[\"Agent\"]"
                        });
                });

            modelBuilder.Entity("Car2Go.Models.AgentCar", b =>
                {
                    b.HasOne("Car2Go.Models.Location", "location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("location");
                });

            modelBuilder.Entity("Car2Go.Models.Car", b =>
                {
                    b.HasOne("Car2Go.Models.Location", "location")
                        .WithMany("cars")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("location");
                });

            modelBuilder.Entity("Car2Go.Models.Payment", b =>
                {
                    b.HasOne("Car2Go.Models.Reservation", "reservation")
                        .WithMany("payments")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("reservation");
                });

            modelBuilder.Entity("Car2Go.Models.Reservation", b =>
                {
                    b.HasOne("Car2Go.Models.Car", "car")
                        .WithMany("reservations")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Car2Go.Models.User", "user")
                        .WithMany("reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("car");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Car2Go.Models.Review", b =>
                {
                    b.HasOne("Car2Go.Models.Car", "car")
                        .WithMany("reviews")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Car2Go.Models.User", "user")
                        .WithMany("reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("car");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Car2Go.Models.Car", b =>
                {
                    b.Navigation("reservations");

                    b.Navigation("reviews");
                });

            modelBuilder.Entity("Car2Go.Models.Location", b =>
                {
                    b.Navigation("cars");
                });

            modelBuilder.Entity("Car2Go.Models.Reservation", b =>
                {
                    b.Navigation("payments");
                });

            modelBuilder.Entity("Car2Go.Models.User", b =>
                {
                    b.Navigation("reservations");

                    b.Navigation("reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
