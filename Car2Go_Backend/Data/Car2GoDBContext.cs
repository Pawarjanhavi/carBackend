using Car2Go.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Car2Go.Data
{
    public class Car2GoDBContext:DbContext
    {
        public Car2GoDBContext() { }

        public Car2GoDBContext(DbContextOptions<Car2GoDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Property Configuration
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(15);

            modelBuilder.Entity<Car>()
                .Property(c => c.PricePerDay)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Review>()
                .Property(r => r.Rating)
                .HasMaxLength(5);

            modelBuilder.Entity<Reservation>()
                .Property(res => res.Amount)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Payment>()
               .Property(p => p.PaymentAmount)
               .HasColumnType("decimal(10,2)");

            //Relationship between the model

            //User -> Reservation (One-to-Many)
            modelBuilder.Entity<Reservation>()
                .HasOne(res => res.user)
                .WithMany(u => u.reservations)
                .HasForeignKey(res => res.UserId);

            //User -> Admin (One-to-One)
            //modelBuilder.Entity<Admin>()
            //    .HasOne(a => a.User)
            //    .WithOne(u => u.admin);

            //User -> Review (One-to-Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.user)
                .WithMany(u => u.reviews)
                .HasForeignKey(r => r.UserId);

            //Car -> Reservation (One-to-Many)
            modelBuilder.Entity<Reservation>()
                .HasOne(res => res.car)
                .WithMany(c => c.reservations)
                .HasForeignKey(res => res.CarId);

            ////Car -> Location (Many-to-One)
            modelBuilder.Entity<Location>()
                .HasMany(l => l.cars)
                .WithOne(c => c.location)
                .HasForeignKey(c => c.LocationId);

            //Car -> Review (One-to-Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.car)
                .WithMany(c => c.reviews)
                .HasForeignKey(r => r.CarId);

            //Reservation -> Payment (One-to-Many)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.reservation)
                .WithMany(res => res.payments)
                .HasForeignKey(p => p.ReservationId);

            //Adding data
            modelBuilder.Entity<User>().HasData(
                new User() { UserId = 1, FirstName = "Zahabiya", LastName = "Kapadia", Email = "zahabiya@gmail.com", Password = "zahabiya@12", PhoneNumber = "9128347653", RoleType = new List<string>() { "Admin" } ,IsDeleted=false},
                new User() {UserId=2, FirstName = "Tanya", LastName = "Patel", Email = "tanya@gmail.com", Password = "tanya@12", PhoneNumber = "9827342651", RoleType = new List<string>() { "User" }, IsDeleted = false },
                new User() { UserId = 3, FirstName = "Rahul", LastName = "Gupta", Email = "rahul@gmail.com", Password = "rahul@123", PhoneNumber = "8837624393", RoleType = new List<string>() { "Agent" }, IsDeleted = false }

                );

            DateTime today = DateTime.Now.Date;
            DateOnly dateOnly = DateOnly.FromDateTime(today);


            modelBuilder.Entity<Car>().HasData(
                new Car() { CarId = 1, Make = "Honda", Model = "Honda City", year = 2020, Colour = "White",TotalSeats =5, LicensePlate = "MP09CP7235", PricePerDay = 3000, ImageUrl= "https://res.cloudinary.com/dhnatfkvb/image/upload/v1733810275/oyqzglkehqgbutmggfas.jpg", AvailableStatus = true,AvailableDate=dateOnly, LocationId = 2 },
                new Car() { CarId = 2, Make = "Honda", Model = "Honda Amaze", year = 2018, Colour = "Red",TotalSeats=5, LicensePlate = "MH50PS2184", PricePerDay = 3200, ImageUrl= "https://res.cloudinary.com/dhnatfkvb/image/upload/v1733810445/honda-amaze-2018-red_vruzbl.jpg", AvailableStatus = true, AvailableDate = dateOnly.AddDays(1), LocationId = 1 },
                new Car() { CarId = 3, Make = "Maruti Suzuki ",Model= " Maruti Suzuki Swift",year = 2021, Colour="Black",TotalSeats=5,LicensePlate= "MP09DY9472",PricePerDay=2000,ImageUrl = "http://res.cloudinary.com/dhnatfkvb/image/upload/v1733813312/o6xkah1rdvc4lytcydhp.webp" ,AvailableStatus = true,AvailableDate = dateOnly.AddDays(2),LocationId=3}
                );

            modelBuilder.Entity<Location>().HasData(
                new Location() { LocationId = 1, Address = "123,Honda Showroom", City = "Mumbai", State = "Maharshtra", Country = "India", ZipCode = "411912" },
                new Location() { LocationId = 2, Address = "711,Honda Showroom", City = "Indore", State = "Madhya Pradesh", Country = "India", ZipCode = "452014" },
                new Location() { LocationId = 3, Address = "Indore", City = "Indore", State = "Madhya Pradesh", Country = "India", ZipCode = "452014" }

                );
            //base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var configSection = configBuilder.GetSection("ConnectionStrings");
            var conn = configSection["DBConnStr"];

            optionsBuilder.UseSqlServer(conn);

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AgentCar> AgentCars { get; set; }
    }
}
