using Microsoft.EntityFrameworkCore;
using Puffix.EFCoreSample.Models;
using System.Threading.Tasks;

namespace Puffix.EFCoreSample.Services
{
    /// <summary>
    /// Data store for cats.
    /// </summary>
    public class CatDataStore : SqliteDataStore<Cat, int> //MemoryDataStore<Cat, int>
    {
        /// <summary>
        /// Set of the items stored in the database.
        /// </summary>
        protected override DbSet<Cat> Items => Cats;

        /// <summary>
        /// Set of cats. Represents teh data in the data store.
        /// </summary>
        public DbSet<Cat> Cats { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databasePath">Path to the file which contains the data.</param>
        public CatDataStore(string databasePath)
            : base(databasePath)
        { }

        /// <summary>
        /// Add a new item in the store.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>Indicates whether the operation is a success or not.</returns>
        public override async Task<bool> AddAsync(Cat item)
        {
            // MemoryDataStore implementation.
            //if (item != null && item.Id == -1)
            //    item.Id = 1 + Items.Max(c => c.Id);

            // Force the ID to 0 for the identity in Sqlite.
            item.Id = 0;
            return await base.AddAsync(item);
        }
    }
}
