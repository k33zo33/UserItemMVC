using Items.Models;

namespace Items.Dal
{
    public interface ICosmosDbService
    {

        //Item
        Task<IEnumerable<Item>> GetItemsAsync(string queryString);
        Task<Item?> GetItemAsync(string id);
        Task AddItemAsync(Item item);
        Task DeleteItemAsync(Item item);
        Task UpdateItemAsync(Item item);

        //User
        Task<IEnumerable<User>> GetUsersAsync(string queryString);
        Task<User?> GetUserAsync(string id);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task UpdateUserAsync(User user);


    }
}
