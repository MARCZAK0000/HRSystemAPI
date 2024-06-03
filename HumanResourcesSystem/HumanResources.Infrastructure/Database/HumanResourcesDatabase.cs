using HumanResources.API.Controllers;
using HumanResources.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Infrastructure.Database
{
    public class HumanResourcesDatabase : IdentityDbContext<User, Roles, string>
    {
        public HumanResourcesDatabase(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Departments> Departments { get; set; } 

        public DbSet<Absence> Absences { get; set; }  

        public DbSet<UserInfo> UserInfo { get; set; }

        public new DbSet<Roles> Roles { get; set; }

        public DbSet<Attendance> Arrivals { get; set; }

        public DbSet<AbsencesType> AbsencesTypes { get; set; }
        
        public DbSet<EmployeePay> EmployeePays { get; set; }

        public DbSet<EmployeePayHistory> EmployeePayHistory {  get; set; }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        public DbSet<AdditionalHours> AdditionalHours { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Departments>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentID)
                .IsRequired();

            builder.Entity<UserInfo>()
                .HasKey(e => e.UserId);

            builder.Entity<Absence>()
                .HasOne(e => e.AbsencesType)
                .WithMany(e => e.Absence)
                .HasForeignKey(e=>e.AbsenceId);

            builder.Entity<EmployeePay>() 
                .HasOne(e => e.User)
                .WithOne(e => e.EmployeePay)
                .HasForeignKey<EmployeePay>(e => e.UserID);

            builder.Entity<EmployeePay>()
                .HasKey(e => e.EmployeePayID);

            builder.Entity<EmployeePayHistory>()
                .HasKey(e => e.EmployeePayHistoryID);

            builder.Entity<EmployeePayHistory>()
                .HasOne(e => e.EmployeePay)
                .WithMany(e => e.EmployeePayHistory)
                .HasForeignKey(e => e.EmpolyeePayID);

            builder.Entity<EmployeePayHistory>()
                .HasOne(e=>e.User)
                .WithMany(e=>e.EmployeePayHistory)
                .HasForeignKey(e=>e.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AdditionalHours>()
                .HasOne(e => e.User)
                .WithMany(e => e.AdditionalHours)
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.Entity<AdditionalHours>()
                .HasOne(e=>e.SuperVisior)
                .WithMany(e=>e.AdditionalHoursSuperVisor)
                .HasForeignKey(e=>e.SuperVisiorID) 
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);


        }
    }
}

