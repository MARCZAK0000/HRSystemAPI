using HumanResources.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HumanResources.Domain.Entities;
using HumanResources.Infrastructure.Repository;
using HumanResources.Infrastructure.SeederDatabase;
using Microsoft.AspNetCore.Identity;
using HumanResources.Infrastructure.Authentication;
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
            service.AddScoped<Domain.Repository.IUserRepository, UserRepository>();
            service.AddScoped<Domain.Repository.IAccountRepository, AccountRepository>();
            service.AddScoped<Domain.Repository.IHelperRepository,  HelperRepository>();

            var authenticationSettings = new AuthenticationSettings();
            configuration.GetSection("Authentication").Bind(authenticationSettings);

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

            service.AddSingleton(authenticationSettings);
        }
    }
}
