using HumanResources.Infrastructure.Database;
using HumarnResource.IntegrationTest.Authentication;
using HumarnResource.IntegrationTest.Database;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumarnResource.IntegrationTest
{
    public class WebApplicationFactoryBuilder:IAsyncLifetime
    {
        private readonly WebApplicationFactory<Program> _factory;
        public HttpClient _client;
        public WebApplicationFactoryBuilder()
        {
            _factory = new WebApplicationFactory<Program>().
                WithWebHostBuilder(builer => 
                    builer.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<HumanResourcesDatabase>));


                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    services.AddMvc(opt => opt.Filters.Add(new FakeUserFilter()));


                    services.AddDbContext<HumanResourcesDatabase>(options =>
                    {
                        options.UseInMemoryDatabase("HumanResourcesDatabase");

                    });
                    services.AddScoped<FakeDatabaseSeeder>();
                }));


            _client=_factory.CreateClient();
        }



        public async Task DisposeAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<HumanResourcesDatabase>();
            
            await context.Database.EnsureDeletedAsync();
            
        }

        public async Task InitializeAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<HumanResourcesDatabase>();
            await context.Database.EnsureCreatedAsync();
            //await context.Roles.AddAsync(FakeDatabaseSeeder.FakeDatabaseSeeder.GetRoles());
            await context.Users.AddAsync(FakeDatabaseSeeder.GetUser());

            await context.SaveChangesAsync();
        }
    }
}
