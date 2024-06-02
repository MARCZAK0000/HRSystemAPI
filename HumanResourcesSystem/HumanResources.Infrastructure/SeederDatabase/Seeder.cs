using HumanResources.Domain.Entities;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Infrastructure.SeederDatabase
{
    public class Seeder : SeederData
    {
        private readonly HumanResourcesDatabase _database;

        private readonly RoleManager<Roles> _roleManager;

 

        public Seeder(HumanResourcesDatabase database, RoleManager<Roles> roleManager)
        {
            _database = database;
            _roleManager = roleManager;

        }

        public async Task Seed()
        {
            if (_database.Database.CanConnect())
            {
                var migrations = await _database.Database.GetPendingMigrationsAsync();
                if(migrations!=null && migrations.Any()) 
                {
                    await _database.Database.MigrateAsync(); 
                }
                if(!_database.Roles.Any()) 
                {
                    var valuesAsList = Enum.GetValues(typeof(Domain.Enums.RolesEnum)).Cast<Domain.Enums.RolesEnum>().ToList();

                    foreach (var item in valuesAsList)
                    {
                        await _roleManager.CreateAsync(new Roles()
                        {
                            Name = item.ToString()
                        });
                    }
                }

                if(!_database.Departments.Any()) 
                {
                    foreach (var item in DeparmentsList)
                    {
                        await _database.Departments.AddAsync(new Departments()
                        {
                            Name = item.ToString(),
                            Description = item.ToString()
                        });
                    }
                    await _database.SaveChangesAsync();
                }

                if (!_database.AbsencesTypes.Any())
                {
                    foreach (var item in AbsencesList)
                    {
                        await _database.AbsencesTypes.AddAsync(new AbsencesType()
                        {
                            Name = item.ToString()
                        });
                    }
                    await _database.SaveChangesAsync();
                }

            }


        }
    }
}
