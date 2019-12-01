using Puffix.ConsoleLogMagnifier;
using Puffix.EFCoreSample.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Puffix.EFCoreSample
{
    /// <summary>
    /// Class to instanciate and manage the lifecycle of the application.
    /// </summary>
    public partial class App : Application
    {
        private static CatDataStore dataStore;

        public static CatDataStore DataStore => dataStore;

        /// <summary>
        /// Constructor.
        /// </summary>
        public App()
        {
            CreateDataStore();

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
        { }

        /// <summary>
        /// Actions when the application goes to sleep.
        /// </summary>
        protected override void OnSleep()
        { }

        /// <summary>
        /// Actions when the application resumes.
        /// </summary>
        protected override void OnResume()
        { }

        private static void CreateDataStore()
        {
            // Build the database file path.
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "cats_data.db");
            bool dbFileExists = File.Exists(databasePath);

            ConsoleHelper.Write($"Create the datastore. The database files({databasePath}) exists? {dbFileExists}.");

            // Initialize the data store.
            var createDataStoreTask = CatDataStore.CreateAsync<CatDataStore>(databasePath);
            Task.WaitAll(createDataStoreTask);
            dataStore = createDataStoreTask.Result;
        }
    }
}
