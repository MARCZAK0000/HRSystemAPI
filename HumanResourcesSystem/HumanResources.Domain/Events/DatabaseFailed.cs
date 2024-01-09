using HumanResources.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Events
{
    public static class DatabaseFailed
    {
        public static void SaveChangesFailed(object? sender, SaveChangesFailedEventArgs e)
        {
            throw new SavingChangesToDatabaseException($"Saving to Database - Failed");
        }
    }
}
