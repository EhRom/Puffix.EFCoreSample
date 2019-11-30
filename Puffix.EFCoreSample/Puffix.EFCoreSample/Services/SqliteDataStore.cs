using Microsoft.EntityFrameworkCore;
using Puffix.EFCoreSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puffix.EFCoreSample.Services
{
    /// <summary>
    /// Data store which stores data with Sqlite.
    /// </summary>
    /// <typeparam name="ItemT">Type of the items stored.</typeparam>
    /// <typeparam name="KeyT">Type of the key for the items in the store.</typeparam>
    public abstract class SqliteDataStore<ItemT, KeyT> : DbContext, IDataStore<ItemT, KeyT>
        where ItemT : class, IItem<KeyT>
    {
        /// <summary>
        /// Set of the items stored in the database.
        /// </summary>
        public abstract DbSet<ItemT> Items { get; set; }

        /// <summary>
        /// Path to the file which contains the data.
        /// </summary>
        private readonly string databasePath;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databasePath">Path to the file which contains the data.</param>
        public SqliteDataStore(string databasePath)
        {
            this.databasePath = databasePath;
        }


        public static async Task<SqliteDataStore<ItemT, KeyT>> CreateAsync<DataSToreT>(string databasePath)
            where DataSToreT : SqliteDataStore<ItemT, KeyT>
        {
            // Get the type of the data store, and create the instance.
            Type dataStoreType = typeof(DataSToreT);

            // Create an instance of that type
            SqliteDataStore<ItemT, KeyT> dataStore = (SqliteDataStore<ItemT, KeyT>)Activator.CreateInstance(dataStoreType, databasePath);

            // Configure the data store.
            await dataStore.Database.EnsureCreatedAsync();
            await dataStore.Database.MigrateAsync();
            return dataStore;
        }

        /// <summary>
        /// Configure the database.
        /// </summary>
        /// <param name="optionsBuilder">Build options.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

        /// <summary>
        /// Add a new item in the store.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>Indicates whether the operation is a success or not.</returns>
        public virtual async Task<bool> AddAsync(ItemT item)
        {
            // TODO test id
            if (await ExistsAsync(item.Id))
                throw new Exception($"Element with id {item.Id} of type {typeof(ItemT).FullName} already exists.");

            await Items.AddAsync(item);

            int result = await (this as DbContext).SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// Update an existing iem in the store.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>Indicates whether the operation is a success or not.</returns>
        public virtual async Task<bool> UpdateAsync(ItemT item)
        {
            if (!await ExistsAsync(item.Id))
                throw new Exception($"Element with id {item.Id} of type {typeof(ItemT).FullName} already exists.");

            Items.Attach(item);

            int result = await (this as DbContext).SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// Delete an item in the store.
        /// </summary>
        /// <param name="id">Id of the item to delete.</param>
        /// <returns>Indicates whether the operation is a success or not.</returns>
        public virtual async Task<bool> DeleteAsync(KeyT id)
        {
            if (!await ExistsAsync(id))
                throw new Exception($"Element with id {id} of type {typeof(ItemT).FullName} already exists.");

            ItemT item = await GetAsync(id);
            Items.Remove(item);

            int result = await (this as DbContext).SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// Test whether a specified item (by id), belongs to the store.
        /// </summary>
        /// <param name="id">Id of the item to test.</param>
        /// <returns>Inidicates whether the item belongs to the store or not.</returns>
        public virtual async Task<bool> ExistsAsync(KeyT id)
        {
            return await Items.Where(i => i.Id.Equals(id)).AnyAsync();
        }

        /// <summary>
        /// Get a specified item (by id), from the store.
        /// </summary>
        /// <param name="id">Id of the item to get.</param>
        /// <returns>The matching item, or the null value, if not found.</returns>
        public virtual async Task<ItemT> GetAsync(KeyT id)
        {
            return await Items.Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get all items of type <typeparamref name="ItemT"/> in the store.
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<ItemT>> GetAllAsync(bool forceRefresh = false)
        {
            return await Items.ToListAsync();
        }
    }
}
