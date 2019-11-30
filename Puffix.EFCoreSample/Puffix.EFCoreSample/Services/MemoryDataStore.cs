using Puffix.EFCoreSample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Puffix.EFCoreSample.Services
{
    /// <summary>
    /// Data store which stores data in memory.
    /// </summary>
    /// <typeparam name="ItemT">Type of the items stored.</typeparam>
    /// <typeparam name="KeyT">Type of the key for the items in the store.</typeparam>
    public abstract class MemoryDataStore<ItemT, KeyT> : IDataStore<ItemT, KeyT>
        where ItemT : IItem<KeyT>
    {
        /// <summary>
        /// Local data store.
        /// </summary>
        protected readonly IDictionary<KeyT, ItemT> itemsCollection;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MemoryDataStore()
        {
            itemsCollection = new Dictionary<KeyT, ItemT>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="items">Collection o the first elements.</param>
        public MemoryDataStore(IEnumerable<ItemT> items)
        {
            itemsCollection = new Dictionary<KeyT, ItemT>();

            foreach (var item in items)
            {
                if(item != null && !itemsCollection.ContainsKey(item.Id))
                {
                    itemsCollection.Add(item.Id, item);
                }
            }
        }

        /// <summary>
        /// Add a new item in the store.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>Indicates whether the operation is a success or not.</returns>
        public virtual Task<bool> AddAsync(ItemT item)
        {
            // Check the item.
            if (item == null || itemsCollection.ContainsKey(item.Id))
                return Task.FromResult(false);

            // Add item in the store.
            itemsCollection.Add(item.Id, item);
            
            return Task.FromResult(true);
        }

        /// <summary>
        /// Update an existing iem in the store.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>Indicates whether the operation is a success or not.</returns>
        public virtual Task<bool> UpdateAsync(ItemT item)
        {
            // Check the item.
            if (item == null || !itemsCollection.ContainsKey(item.Id))
                return Task.FromResult(false);

            // Update item in the store.
            itemsCollection[item.Id] = item;

            return Task.FromResult(true);
        }

        /// <summary>
        /// Delete an item in the store.
        /// </summary>
        /// <param name="id">Id of the item to delete.</param>
        /// <returns>Indicates whether the operation is a success or not.</returns>
        public virtual Task<bool> DeleteAsync(KeyT id)
        { 
            // Check the item.
            if (!itemsCollection.ContainsKey(id))
                return Task.FromResult(false);

            // Delete the item from the store.
            itemsCollection.Remove(id);

            return Task.FromResult(true);
        }

        /// <summary>
        /// Get a specified item (by id), from the store.
        /// </summary>
        /// <param name="id">Id of the item to get.</param>
        /// <returns>The matching item, or the null value, if not found.</returns>
        public virtual Task<bool> ExistsAsync(KeyT id)
        {
            // Check the item.
            if (!itemsCollection.ContainsKey(id))
                return Task.FromResult(false);

            return Task.FromResult(true);
        }

        /// <summary>
        /// Get a specified item (by id), from the store.
        /// </summary>
        /// <param name="id">Id of the item to get.</param>
        /// <returns>The matching item, or the null value, if not found.</returns>
        public virtual Task<ItemT> GetAsync(KeyT id)
        {
            // Check the item.
            if (!itemsCollection.ContainsKey(id))
                return Task.FromResult(default(ItemT));

            return Task.FromResult(itemsCollection[id]);
        }

        /// <summary>
        /// Get all items of type <typeparamref name="ItemT"/> in the store.
        /// </summary>
        /// <param name="forceRefresh">Indicates whether to refresh the list or not.</param>
        /// <returns>Items in the store.</returns>
        public Task<IEnumerable<ItemT>> GetAllAsync(bool forceRefresh = false)
        {
            return Task.FromResult(itemsCollection.Values as IEnumerable<ItemT>);
        }
    }
}
