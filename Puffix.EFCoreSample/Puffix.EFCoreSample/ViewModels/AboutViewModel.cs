using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Puffix.EFCoreSample.ViewModels
{
    /// <summary>
    /// About view model.
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        /// <summary>
        /// Application name.
        /// </summary>
        public string ApplicationName => AppInfo.Name;

        /// <summary>
        /// Version of the application.
        /// </summary>
        public string Version => AppInfo.VersionString;

        /// <summary>
        /// Command to open the wamarin web site.
        /// </summary>
        public ICommand OpenXamarinWebSiteCommand { get; }

        /// <summary>
        /// Command to open the puffix.io web site.
        /// </summary>
        public ICommand OpenPuffixWebSiteCommand { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AboutViewModel()
        {
            Title = "About";

            OpenXamarinWebSiteCommand = new Command(async () => await OpenWebUriAsync(new Uri("https://xamarin.com/platform")));
            OpenPuffixWebSiteCommand = new Command(async () => await OpenWebUriAsync(new Uri("https://puffix.io/")));
        }

        /// <summary>
        /// Open an URI.
        /// </summary>
        /// <param name="uri">URI to open.</param>
        public async Task OpenWebUriAsync(Uri uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.External);
        }
    }
}