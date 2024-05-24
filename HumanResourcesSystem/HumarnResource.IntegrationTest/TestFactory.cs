using HumanResources.Infrastructure.Database;
using HumarnResource.IntegrationTest.Authentication;
using HumarnResource.IntegrationTest.FakeDatabaseSeeder;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumarnResource.IntegrationTest
{
    internal class TestFactory : WebApplicationFactory<Program>
    {
        protected override async void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(async services =>
            {
                //Remove current database
                services.RemoveAll(typeof(DbContextOptions<HumanResourcesDatabase>));

                //Connect to database
                //services.AddSqlServer<HumanResourcesDatabase>("server=LAPTOP-AGSJTTQJ; database = TestHumanResourcesDb;trusted_connection=true;encrypt=false;");

                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                services.AddMvc(opt => opt.Filters.Add(new FakeUserFilter()));

                services.AddDbContext<HumanResourcesDatabase>(options =>
                {
                    options.UseInMemoryDatabase("HumanResourcesDatabase");

                });
                services.AddScoped<FakeDatabaseSeeder.FakeDatabaseSeeder>();

                var serviceProvider = services.BuildServiceProvider();
                var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<FakeDatabaseSeeder.FakeDatabaseSeeder>();
                await dbContext.Seed();

            });

        }


        

    }
}
