using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using HumanResources.Domain.CalculateDays;
using HumanResources.Domain.CurrencyData;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Authentication;
using HumanResources.Infrastructure.Database;
using HumanResources.Infrastructure.Repository;
using HumanResources.Infrastructure.SeederDatabase;
using HumanResources.Infrastructure.StorageAccount;
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
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration, bool isDevlopment)
        {

            if(isDevlopment) 
            {
                service.AddDbContext<HumanResourcesDatabase>(options=> options.UseSqlServer(configuration.GetConnectionString("MyConnectionString")));
                service.AddSingleton(new BlobServiceClient(configuration.GetConnectionString("StorageAccountEmulator"), 
                    new BlobClientOptions(BlobClientOptions.ServiceVersion.V2019_02_02)));
                service.AddSingleton(new QueueServiceClient(configuration.GetConnectionString("StorageAccountEmulator"), 
                    new QueueClientOptions(QueueClientOptions.ServiceVersion.V2019_02_02)));
                service.AddSingleton(new TableServiceClient(configuration.GetConnectionString("StorageAccountEmulator"),
                    new TableClientOptions(TableClientOptions.ServiceVersion.V2019_02_02)));
                
            }
            else
            {
                service.AddDbContext<HumanResourcesDatabase>(options => options.UseSqlServer(configuration.GetConnectionString("HumanResourcesDbConnection")));
                service.AddSingleton(new BlobServiceClient(configuration.GetConnectionString("HumanResourcesStrorageAccountConnection")));
                service.AddSingleton(new QueueServiceClient(configuration.GetConnectionString("HumanResourcesStrorageAccountConnection")));
                service.AddSingleton(new TableServiceClient(configuration.GetConnectionString("HumanResourcesStrorageAccountConnection")));
            }
            service.AddIdentity<User, Roles>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<HumanResourcesDatabase>()
            .AddDefaultTokenProviders();


            service.AddScoped<Seeder>();
            service.AddScoped<AdminTableSeeder>();
            service.AddScoped<IAccountReposiotry, AccountRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IHelperRepository, HelperRepository>();
            service.AddScoped<IAttendanceRepository, AttendanceRepository>();
            service.AddScoped<IAdminRepository, AdminRepository>();
            service.AddScoped<IEmailRepostiory, EmailRepository>();
            service.AddScoped<IAbsenceRepository, AbsenceRepository>();
            service.AddScoped<IDepartmentReposiotry, DepartmentRepository>();
            service.AddScoped(typeof(IPDFReportRepository<>), typeof(PDFReportRepository<>));
            service.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
            service.AddScoped<CurrencyFactory>();
            service.AddScoped<CalculateFactory>();
            service.AddScoped<IEmployeePayRepository, EmployeePayRepository>();
            service.AddScoped<IAdditionalHoursReposiotry, AdditionalHoursReposiotry>();
            service.AddScoped<IBlobClientReposiotry, BlobClientReposiotry>();

            var exchangeRateAPI = new ExchangeRateAPIAuthenticationSettings();
            configuration.GetSection("ExchangeRate_API").Bind(exchangeRateAPI);
            service.AddSingleton(exchangeRateAPI);

            var calculateDays = new CalculateDays();
            configuration.GetSection("Calculate_Days_of_Absence").Bind(calculateDays);
            service.AddSingleton(calculateDays);

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

            var storageAccountSettings = new StorageAccountSettings();
            configuration.GetSection("StorageAccountSettings").Bind(storageAccountSettings);
            service.AddSingleton(storageAccountSettings);

        }
    }
}
