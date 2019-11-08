using Puffix.EFCoreSample.Models;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Puffix.EFCoreSample.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewCatPage : ContentPage
    {
        public Cat Cat { get; set; }

        public NewCatPage()
        {
            InitializeComponent();

            Cat = new Cat
            {
                Name = "Cat name",
                Color = "Cat color",
                BirthDate = DateTime.Now.AddYears(-7)
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Cat.Id = -1;
            MessagingCenter.Send(this, "AddCat", Cat);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}