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

        public DbSet<Arrivals> Arrivals { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Departments>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentID)
                .IsRequired();

            builder.Entity<UserInfo>()
                .HasMany(e=>e.Absences)
                .WithOne(e => e.User)
                .HasForeignKey(e=>e.UserId)
                .IsRequired();

           

            base.OnModelCreating(builder);

        }
    }
}

