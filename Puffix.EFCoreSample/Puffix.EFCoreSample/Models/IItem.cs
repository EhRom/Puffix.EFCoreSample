namespace Puffix.EFCoreSample.Models
{
    /// <summary>
    /// Contract for items.
    /// </summary>
    /// <typeparam name="KeyT">Type of the key of the item.</typeparam>
    public interface IItem<KeyT>
    {
        /// <summary>
        /// Id of the item.
        /// </summary>
        KeyT Id { get; set; }
    }
}
