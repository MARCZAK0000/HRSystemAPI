using HumanResources.Domain.Entities;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Authentication;
using HumanResources.Infrastructure.Database;
using HumanResources.Infrastructure.Repository;
using HumanResources.Infrastructure.SeederDatabase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HumanResources.Infrastructure.Extension
{
    public static class ServiceExtension
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<HumanResourcesDatabase>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("MyConnectionString"));

            });


            service.AddIdentity<User, Roles>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<HumanResourcesDatabase>()
            .AddDefaultTokenProviders();


            service.AddScoped<Seeder>();
            service.AddScoped<IAccountReposiotry, AccountRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IHelperRepository, HelperRepository>();
            service.AddScoped<IAttendanceRepository, AttendanceRepository>();
            service.AddScoped<IAdminRepository, AdminRepository>();
            service.AddScoped<IEmailRepostiory, EmailRepository>();
            service.AddScoped<IAbsenceRepository, AbsenceRepository>();
            service.AddScoped<IDepartmentReposiotry, DepartmentRepository>();

            var emailAuthenticationSettings = new EmailAuthenticationSettings();
            configuration.GetSection("EmailAuthentication").Bind(emailAuthenticationSettings);  //Register IN DI

            service.AddSingleton(emailAuthenticationSettings); //!!!!!!!!!!!!!!!!!

            var authenticationSettings = new AuthenticationSettings();
            configuration.GetSection("Authentication").Bind(authenticationSettings);

            service.AddSingleton(authenticationSettings); //!!!!!!!!!!!!!!!!!

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });

        }
    }
}
