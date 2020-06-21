using EducUp.Common;
using EducUp.Service;
using EducUp.View;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace EducUp
{
    public partial class App : Application
    {
        public static IDataService DataService;

        public App()
        {
            InitializeComponent();

            DataService = CloudFirestoreSerivice.GetInstance();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        /// <summary>
        /// Logga sul canale di Debug l'eccezione passata come parametro.
        /// Da usare nei blocchi catch
        /// </summary>
        /// <param name="e">
        /// Eccezione generata da loggare
        /// </param>
        public static void LogException(Exception e)
        {
            Debug.WriteLine(e.Message);
            Debug.WriteLine(e.StackTrace);
        }
    
    }
}
