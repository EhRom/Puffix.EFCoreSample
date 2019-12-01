using Puffix.EFCoreSample.Models;

namespace Puffix.EFCoreSample.ViewModels
{
    /// <summary>
    /// View model for a cat.
    /// </summary>
    public class CatDetailViewModel : BaseViewModel
    {
        /// <summary>
        /// Cat whih is displayed.
        /// </summary>
        public Cat Cat { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cat">Cat to display.</param>
        public CatDetailViewModel(Cat cat = null)
        {
            Title = cat?.Name;
            Cat = cat;
        }
    }
}
