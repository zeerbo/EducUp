using EducUp.Common;
using EducUp.Service;
using EducUp.Utils;
using EducUp.View;
using Plugin.FirebaseAuth;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EducUp
{
    public partial class App : Application
    {
        public static IDataService DataService;
        public static IAuthService AuthService;

        public App()
        {
            InitializeComponent();

            if (!DesignMode.IsDesignModeEnabled)
            {
                DataService = CloudFirestoreSerivice.GetInstance();
                AuthService = FirebaseAuthenticationService.GetInstance(); 
            }

            string username = Preferences.Get(Constants.EMAIL_PREFERENCE, string.Empty);
            string password = Preferences.Get(Constants.PASSWORD_PREFERENCE, string.Empty);

            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
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
    
        public static async Task<bool> LoginUserAync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;

            return await AuthService.SignInWhitEmailAndPassword(email, password);
        }

        public static bool SaveCredentials(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;

            Preferences.Set(Constants.EMAIL_PREFERENCE, email);
            Preferences.Set(Constants.PASSWORD_PREFERENCE, password);

            return true;
        }
    }
}
