using Puffix.EFCoreSample.Models;
using System.Threading.Tasks;

namespace Puffix.EFCoreSample.Services
{
    /// <summary>
    /// Data store for cats.
    /// </summary>
    public class CatDataStore : MemoryDataStore<Cat, int>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CatDataStore()
        { }

        /// <summary>
        /// Add a new item in the store.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>Indicates whether the operation is a success or not.</returns>
        public override Task<bool> AddItemAsync(Cat item)
        {
            if (item != null && item.Id == -1)
                item.Id = itemsCollection.Count + 1;
        
            return base.AddItemAsync(item);
        }
    }
}
