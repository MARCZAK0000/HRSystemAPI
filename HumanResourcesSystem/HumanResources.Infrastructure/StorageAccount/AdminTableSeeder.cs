using Azure.Data.Tables;

namespace HumanResources.Infrastructure.StorageAccount
{
    public class AdminTableSeeder
    {
        private readonly StorageAccountSettings _storageAccountSettings;

        private readonly TableServiceClient _tableServiceClient;

        public AdminTableSeeder(StorageAccountSettings storageAccountSettings, TableServiceClient tableServiceClient)
        {
            _storageAccountSettings = storageAccountSettings;
            _tableServiceClient = tableServiceClient;
        }

        public async Task CreateAdminContainerIfNotExistAsync()
        {
            await _tableServiceClient.CreateTableIfNotExistsAsync(_storageAccountSettings.TableContinerName);
        }
    }
}
