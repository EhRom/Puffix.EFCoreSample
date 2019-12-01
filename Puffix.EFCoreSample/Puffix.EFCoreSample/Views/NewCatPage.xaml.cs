using Puffix.EFCoreSample.Models;
using Puffix.EFCoreSample.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Puffix.EFCoreSample.Views
{
    /// <summary>
    /// Page to add a new cat.
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class NewCatPage : ContentPage
    {
        /// <summary>
        /// Cat view model.
        /// </summary>
        private readonly CatDetailViewModel viewModel;

        /// <summary>
        /// Constructor.
        /// </summary>
        public NewCatPage()
        {
            Cat newCat = new Cat
            {
                Name = "Cat name",
                Color = "Cat color",
                BirthDate = DateTime.Now.AddYears(-7)
            };
            viewModel = new CatDetailViewModel(newCat);

            InitializeComponent();
            
            BindingContext = viewModel;
        }

        /// <summary>
        /// Action when the button Save is pressed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
       private  async void Save_Clicked(object sender, EventArgs e)
        {
            viewModel.Cat.Id = -1;
            MessagingCenter.Send(this, "AddCat", viewModel.Cat);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Action when the button Cancel is pressed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}