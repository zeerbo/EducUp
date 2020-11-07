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
        public static IPresenceNotificationService PresenceNotificationService;

        public App()
        {
            InitializeComponent();

            if (!DesignMode.IsDesignModeEnabled)
            {
                DataService = CloudFirestoreSerivice.GetInstance();
                AuthService = FirebaseAuthenticationService.GetInstance();
                PresenceNotificationService = DependencyService.Get<IPresenceNotificationService>();
            }

            string username = GetUserEmail();
            string password = Preferences.Get(Constants.PASSWORD_PREFERENCE, string.Empty);

            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                MainPage = new AppMasterDetailPage(); ;
            }
            else
            {
                MainPage = new LoginPage();
            }
        }
        
        
        #region Overrides

        protected override void OnStart()
        {
        }

        protected override async void OnSleep()
        {
            await PresenceNotificationService.UnPublishPresenceNotificationAsync();
        }

        protected override async void OnResume()
        {
            string presenceId = GetPresenceId();
            if (PresenceNotificationService != null && !string.IsNullOrEmpty(presenceId))
            {
                await PresenceNotificationService.PublishPresenceNotificationAsync(presenceId);
            }
        }

        #endregion

        #region Methods

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
            bool result = false;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                Preferences.Set(Constants.EMAIL_PREFERENCE, email);
                Preferences.Set(Constants.PASSWORD_PREFERENCE, password);
                result = true;
            }

            return result;
        }

        public static string GetUserEmail()
        {
            return Preferences.Get(Constants.EMAIL_PREFERENCE, string.Empty);
        }

        public static string GetNewGuid()
        {
            string result = string.Empty;

            try
            {
                Guid guid = Guid.NewGuid();
                result = guid.ToString();
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        public static void SaveAdminProfile(bool isAdminProfile)
        {
            Preferences.Set(Constants.ADMIN_PREFERENCE, isAdminProfile);
        }

        public static bool IsAdminProfile()
        {
            return Preferences.Get(Constants.ADMIN_PREFERENCE, false);
        }

        public static void SaveUserPresenceId(string presenceId)
        {
            Preferences.Set(Constants.PRESENCE_ID, presenceId);
        }

        public static string GetPresenceId()
        {
            return Preferences.Get(Constants.PRESENCE_ID, string.Empty);
        }

        #endregion
    }
}
