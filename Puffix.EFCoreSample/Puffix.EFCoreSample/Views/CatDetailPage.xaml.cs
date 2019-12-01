using Puffix.EFCoreSample.Models;
using Puffix.EFCoreSample.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Puffix.EFCoreSample.Views
{
    /// <summary>
    /// Page to display the cat's details.
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class CatDetailPage : ContentPage
    {
        /// <summary>
        /// Cat view model.
        /// </summary>
        private readonly CatDetailViewModel viewModel;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CatDetailPage()
        {
            var cat = new Cat
            {
                Name = "New cat name",
                Color = "Cat's color."
            };

            viewModel = new CatDetailViewModel(cat);
            InitializeComponent();

            BindingContext = viewModel;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="viewModel">Cat view model.</param>
        public CatDetailPage(CatDetailViewModel viewModel)
        {
            this.viewModel = viewModel;

            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = viewModel;
        }

        /// <summary>
        /// Action when the button RemoveCat is pressed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void RemoveCat_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "RemoveCat", viewModel.Cat);
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Action when the button Cancel is pressed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}