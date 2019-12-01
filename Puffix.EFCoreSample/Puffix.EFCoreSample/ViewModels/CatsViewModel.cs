using Puffix.EFCoreSample.Models;
using Puffix.EFCoreSample.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Puffix.EFCoreSample.ViewModels
{
    /// <summary>
    /// Cats view model.
    /// </summary>
    public class CatsViewModel : BaseViewModel
    {
        /// <summary>
        /// List of the cats displayed.
        /// </summary>
        public ObservableCollection<Cat> Cats { get; set; }

        /// <summary>
        /// Command to load cats.
        /// </summary>
        public Command LoadCatsCommand { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CatsViewModel()
        {
            Title = "Browse";
            Cats = new ObservableCollection<Cat>();
            LoadCatsCommand = new Command(async () => await ExecuteLoadCatsCommand());

            MessagingCenter.Subscribe<NewCatPage, Cat>(this, "AddCat", async (obj, cat) =>
            {
                var newCat = cat as Cat;
                Cats.Add(newCat);
                if (DataStore != null)
                    await DataStore.AddAsync(newCat);
            });
            MessagingCenter.Subscribe<CatDetailPage, Cat>(this, "RemoveCat", async (obj, cat) =>
            {
                var catToRemove = cat as Cat;

                if (DataStore != null)
                    await DataStore.DeleteAsync(catToRemove.Id);
            });
        }

        /// <summary>
        /// Execute the LoadCats command.
        /// </summary>
        /// <returns>Asynchronous task.</returns>
        private async Task ExecuteLoadCatsCommand()
        {
            try
            {
                Cats.Clear();
                if (DataStore != null)
                {
                    var cats = await DataStore.GetAllAsync();

                    foreach (var cat in cats)
                    {
                        Cats.Add(cat);
                    }
                }
            }
            catch (Exception error)
            {
                Debug.WriteLine(error);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}