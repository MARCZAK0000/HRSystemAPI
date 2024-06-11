using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.StorageAccountModel
{
    public class AdminTableEntity : ITableEntity
    {
        public string PartitionKey { get ; set ; }
        public string RowKey { get ; set ; }
        public DateTimeOffset? Timestamp { get ; set ; }
        public ETag ETag { get ; set ; }

        public string Email { get ; set ; }
        public string UserID { get ; set ; }
        public string UserCode { get ; set ; }  
        public string Key { get ; set ; }
    }
}
