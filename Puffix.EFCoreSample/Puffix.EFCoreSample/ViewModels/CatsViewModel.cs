using Puffix.EFCoreSample.Models;
using Puffix.EFCoreSample.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Puffix.EFCoreSample.ViewModels
{
    public class CatsViewModel : BaseViewModel
    {
        public ObservableCollection<Cat> Cats { get; set; }
        public Command LoadItemsCommand { get; set; }

        public CatsViewModel()
        {
            Title = "Browse";
            Cats = new ObservableCollection<Cat>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewCatPage, Cat>(this, "AddCat", async (obj, cat) =>
            {
                var newCat = cat as Cat;
                Cats.Add(newCat);
                await DataStore.AddItemAsync(newCat);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Cats.Clear();
                var cats = await DataStore.GetItemsAsync(true);
                foreach (var cat in cats)
                {
                    Cats.Add(cat);
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