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

        public DbSet<AbsencesType> AbsencesTypes { get; set; }
        

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

            base.OnModelCreating(builder);


        }
    }
}

