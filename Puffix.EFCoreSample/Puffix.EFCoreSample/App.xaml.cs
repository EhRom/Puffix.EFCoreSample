using Puffix.EFCoreSample.Services;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Puffix.EFCoreSample
{
    /// <summary>
    /// Class to instanciate and manage the lifecycle of the application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Root application path.
        /// </summary>
        private readonly string rootApplicationPath;

        private static CatDataStore dataStore;

        public static CatDataStore DataStore => dataStore;

        /// <summary>
        /// Constructor.
        /// </summary>
        public App(string rootApplicationPath)
        {
            this.rootApplicationPath = rootApplicationPath;

            // Initialze the application.
            InitializeComponent();

            // TODO vNext use Autofac for IoC.
            //DependencyService.Register<CatDataStore>();

            // Initializa the app shell.
            MainPage = new AppShell();
        }

        /// <summary>
        /// Actions when the application starts.
        /// </summary>
        protected override void OnStart()
        {
            // Initialize the data store.
            string databasePath = Path.Combine(this.rootApplicationPath, "cats_data.db3");
            //dataStore = new CatDataStore();

            var createDataStoreTask = CatDataStore.CreateAsync<CatDataStore>(databasePath);
            Task.WaitAll(createDataStoreTask);
            dataStore = (CatDataStore)createDataStoreTask.Result;
        }

        /// <summary>
        /// Actions when the application goes to sleep.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Actions when the application resumes.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
