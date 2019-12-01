using Puffix.EFCoreSample.Models;
using Puffix.EFCoreSample.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Puffix.EFCoreSample.Views
{
    /// <summary>
    /// Page for displaying the list of the cats.
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class CatsPage : ContentPage
    {
        /// <summary>
        /// Cats view model.
        /// </summary>
        private readonly CatsViewModel viewModel;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CatsPage()
        {
            InitializeComponent();

            viewModel = new CatsViewModel();
            BindingContext = viewModel;
        }

        /// <summary>
        /// Action when a cat is selected.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void OnCatSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var cat = args.SelectedItem as Cat;
            if (cat == null)
                return;

            await Navigation.PushAsync(new CatDetailPage(new CatDetailViewModel(cat)));

            // Manually deselect item.
            CatsListView.SelectedItem = null;
        }

        /// <summary>
        /// Action when the button AddCat is pressed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        async void AddCat_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCatPage()));
        }

        /// <summary>
        /// Action when the page is appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadCatsCommand.Execute(null);
        }
    }
}