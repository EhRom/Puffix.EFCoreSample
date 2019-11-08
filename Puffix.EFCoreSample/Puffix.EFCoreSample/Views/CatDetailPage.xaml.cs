using Puffix.EFCoreSample.Models;
using Puffix.EFCoreSample.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Puffix.EFCoreSample.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CatDetailPage : ContentPage
    {
        CatDetailViewModel viewModel;

        public CatDetailPage(CatDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public CatDetailPage()
        {
            InitializeComponent();

            var cat = new Cat
            {
                Name = "New cat name",
                Color = "Cat's color."
            };

            viewModel = new CatDetailViewModel(cat);
            BindingContext = viewModel;
        }
    }
}