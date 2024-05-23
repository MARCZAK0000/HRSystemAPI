using HumanResources.Domain.Entities;
using HumanResources.Infrastructure.Database;

namespace HumarnResource.IntegrationTest.FakeDatabaseSeeder
{
    public class FakeDatabaseSeeder
    {
        private readonly HumanResourcesDatabase _database;
        public FakeDatabaseSeeder(HumanResourcesDatabase database) 
        {
            _database = database;
        }

        public async Task Seed()
        {
            if(await  _database.Database.CanConnectAsync()) 
            {
                if(!_database.Users.Any()) 
                {
                    await _database.Users.AddAsync(new User()
                    {
                        Id="100",
                        Email = "test@example.com",
                        UserCode="0000000000000",
                        UserName = "test@example.com",
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0,
                        RefreshToken = "0000",
                        UserInfo = new UserInfo()
                        {
                            UserId = "100",
                            Name = "Test",
                            LastName = "Test",
                            UserCode = "0000000000000",
                            DaysOfAbsencesToUse = 28,
                            DaysOfAbsencesCurrentYear = 0,
                            YearsOfExperiences = 2,
                            EducationTitle = HumanResources.Domain.Enums.EducationLevel.Higher,
                            DepartmentID = 9

                        }

                    });
                }
            }
            await _database.SaveChangesAsync();
        }
    }
}
