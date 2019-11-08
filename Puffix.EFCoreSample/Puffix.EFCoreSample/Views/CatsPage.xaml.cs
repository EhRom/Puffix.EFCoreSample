using Puffix.EFCoreSample.Models;
using Puffix.EFCoreSample.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Puffix.EFCoreSample.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CatsPage : ContentPage
    {
        private CatsViewModel viewModel;

        public CatsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CatsViewModel();
        }

        async void OnCatSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var cat = args.SelectedItem as Cat;
            if (cat == null)
                return;

            await Navigation.PushAsync(new CatDetailPage(new CatDetailViewModel(cat)));

            // Manually deselect item.
            CatsListView.SelectedItem = null;
        }

        async void AddCat_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCatPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Cats.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}