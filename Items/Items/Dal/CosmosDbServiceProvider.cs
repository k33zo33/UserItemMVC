using Microsoft.Azure.Cosmos;

namespace Items.Dal
{
    public class CosmosDbServiceProvider
    {
        private const string DatabaseName = "";
        private const string ContainerName = "";
        private const string Account = "";
        private const string Key = "";

        private static ICosmosDbService? service;

        public static ICosmosDbService? Service { get => service;}
        public static async Task Init()
        {
            CosmosClient cosmosClient = new(Account, Key);
            service = new CosmosDbService(cosmosClient, DatabaseName, ContainerName);
            DatabaseResponse databaseResponse =
                await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await databaseResponse.Database.CreateContainerIfNotExistsAsync(ContainerName, "/id");

        }
    }
}
