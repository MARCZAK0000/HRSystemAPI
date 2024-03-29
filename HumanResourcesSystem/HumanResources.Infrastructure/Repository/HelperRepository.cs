﻿using HumanResources.Domain.Exceptions;
using HumanResources.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Infrastructure.Repository
{
    public class HelperRepository : Domain.Repository.IHelperRepository
    {

        private readonly HumanResourcesDatabase _database;

        public HelperRepository(HumanResourcesDatabase Database)
        {
            _database = Database;
        }
        public async Task<string> GenerateRandomUserCode()
        {
            var chars = "0123456789";

            var code = new string(Enumerable.Repeat(chars, 12).Select(s => s[Random.Shared.Next(chars.Length)]).ToArray());

            var codeList = await _database.Users.FirstOrDefaultAsync(pr=>pr.UserCode == code);
            if(codeList != null) 
            {
                throw new ServerErrorException("Generate User Code: Problem with code generation, try again later");
            }
            return code;
        }
    }
}
