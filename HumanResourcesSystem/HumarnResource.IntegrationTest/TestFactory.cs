using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumarnResource.IntegrationTest
{
    internal class TestFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                //Remove current database
                services.RemoveAll(typeof(DbContextOptions<HumanResourcesDatabase>));

                //Connect to database
                services.AddSqlServer<HumanResourcesDatabase>("server=LAPTOP-AGSJTTQJ; database = TestHumanResourcesDb;trusted_connection=true;encrypt=false;");             
            });

            
        }
       
    }
}
