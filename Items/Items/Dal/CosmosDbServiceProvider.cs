using Microsoft.Azure.Cosmos;

namespace Items.Dal
{
    public class CosmosDbServiceProvider
    {
        private const string DatabaseName = "pppkcosmosdb";
        private const string ContainerName = "Todo";
        private const string Account = "https://pppkcosmosdb.documents.azure.com:443/";
        private const string Key = "YJN5uMdHLGVpahLl2OKf0SSVMWLEpBnES8gqS8WpIgx1nLLCFRaI6Id8WK8RvutiKOqFR8SalQU6ACDbPmZ72A==";

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
