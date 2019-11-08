using Puffix.EFCoreSample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Puffix.EFCoreSample.Services
{
    /// <summary>
    /// Data store contract.
    /// </summary>
    /// <typeparam name="ItemT">Type of the items stored.</typeparam>
    /// <typeparam name="KeyT">Type of the key for the items in the store.</typeparam>
    public interface IDataStore<ItemT, KeyT>
        where ItemT : IItem<KeyT>
    {
        /// <summary>
        /// Add a new item in the store.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>Indicates whether the operation is a success or not.</returns>
        Task<bool> AddItemAsync(ItemT item);

        /// <summary>
        /// Update an existing iem in the store.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>Indicates whether the operation is a success or not.</returns>
        Task<bool> UpdateItemAsync(ItemT item);

        /// <summary>
        /// Delete an item in the store.
        /// </summary>
        /// <param name="id">Id of the item to delete.</param>
        /// <returns>Indicates whether the operation is a success or not.</returns>
        Task<bool> DeleteItemAsync(KeyT id);

        /// <summary>
        /// Get a specified item (by id), from the store.
        /// </summary>
        /// <param name="id">Id of the item to get.</param>
        /// <returns>The matching item, or the null value, if not found.</returns>
        Task<ItemT> GetItemAsync(KeyT id);

        /// <summary>
        /// Get all items of type <typeparamref name="ItemT"/> in the store.
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        Task<IEnumerable<ItemT>> GetItemsAsync(bool forceRefresh = false);
    }
}
