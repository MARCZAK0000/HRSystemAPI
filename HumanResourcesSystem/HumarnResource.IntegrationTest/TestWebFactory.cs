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
    public static class TestWebFactory
    {
       

        public static HttpClient ReturnClient(WebApplicationFactory<Program> factory)
        {
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<HumanResourcesDatabase>));


                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    services.AddMvc(opt => opt.Filters.Add(new FakeUserFilter()));


                    services.AddDbContext<HumanResourcesDatabase>(options =>
                    {
                        options.UseInMemoryDatabase("HumanResourcesDatabase");

                    });
                    services.AddScoped<FakeDatabaseSeeder.FakeDatabaseSeeder>();


                    GetFakeDatabase(services);
                });
            }).CreateClient();

            return client;
        }

        

        private static HumanResourcesDatabase DeleteDatabase(IServiceCollection services)
        { 
            var serviceProvider = services.BuildServiceProvider().CreateScope();
            var dbContext = serviceProvider.ServiceProvider.GetRequiredService<HumanResourcesDatabase>();

            return dbContext;
        }

        private static void GetFakeDatabase(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<FakeDatabaseSeeder.FakeDatabaseSeeder>();

            dbContext.Seed();
        }

    }
}
