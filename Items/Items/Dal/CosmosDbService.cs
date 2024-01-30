using Items.Models;
using Microsoft.Azure.Cosmos;
using System;


namespace Items.Dal
{
    public class CosmosDbService : ICosmosDbService
    {

        private readonly Container container;


        
        public CosmosDbService(CosmosClient cosmosClient, string dbName, string cntName)
        {
            container = cosmosClient.GetContainer(dbName, cntName);
        }


        //Item
        public async Task AddItemAsync(Item item)
            => await container.CreateItemAsync(item,new PartitionKey(item.Id));

        public async Task UpdateItemAsync(Item item)
          => await container.UpsertItemAsync(item, new PartitionKey(item.Id));

        public async Task DeleteItemAsync(Item item)
            => await container.DeleteItemAsync<Item>(item.Id, new PartitionKey(item.Id));

       

        public async Task<Item?> GetItemAsync(string id)
        {
            try
            {
                return await container.ReadItemAsync<Item>(id, new PartitionKey(id));
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return null;
            }
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(string queryString)
        {

            List<Item> items = new List<Item>();

            var query = container.GetItemQueryIterator<Item>(new QueryDefinition(queryString));

            while (query.HasMoreResults)
            {
                var result = await query.ReadNextAsync();
                result.ToList().ForEach(r =>
                {
                    if (r.Type == nameof(Item))
                    {
                        items.Add(r);
                    }
                });
            }

            return items;

        }




        //User 
        public async Task AddUserAsync(Models.User user)
          => await container.CreateItemAsync(user, new PartitionKey(user.Id));

        public async Task UpdateUserAsync(Models.User user)
          => await container.UpsertItemAsync(user, new PartitionKey(user.Id));

        public async Task DeleteUserAsync(Models.User user)
           => await container.DeleteItemAsync<Models.User>(user.Id, new PartitionKey(user.Id));



        public async Task<Models.User?> GetUserAsync(string id)
        {
            try
            {
                return await container.ReadItemAsync<Models.User>(id, new PartitionKey(id));
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return null;
            }
        }

        public async Task<IEnumerable<Models.User>> GetUsersAsync(string queryString)
        {


            List<Models.User> users = new List<Models.User>();

            var query = container.GetItemQueryIterator<Models.User>(new QueryDefinition(queryString));

            while (query.HasMoreResults)
            {
                var result = await query.ReadNextAsync();
                result.ToList().ForEach(r =>
                {
                    if (r.Type == nameof(Models.User))
                    {
                        users.Add(r);
                    }
                });
            }

            return users;

        }

   
    }
}
