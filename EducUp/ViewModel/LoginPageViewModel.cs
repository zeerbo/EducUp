using EducUp.Model;
using EducUp.Utils;
using EducUp.View;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EducUp.ViewModel
{
    public class LoginPageViewModel : ObservableObject
    {
        #region Properties

        public Command<object> AcLabelTapped { get; set; }
        
        public Command<object> RegistrationLabelTapped { get; set; }

        #endregion


        #region Constructor

        public LoginPageViewModel()
        {
            AcLabelTapped = new Command<object>(OpenAcBoSite);
            RegistrationLabelTapped = new Command<object>(OpenRegistrationPage);
        }

        #endregion


        #region PublicMethod

        public async Task<bool> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;

            bool result = await App.LoginUserAync(email, password);

            if (result)
            {
                App.SaveCredentials(email, password);

                User user = await App.DataService.GetUserAsync(email);
                if(user != null)
                {
                    App.SaveAdminProfile(user.IsAdmin);
                }
            }

            return result;
        }

        #endregion

        #region PrivateMethod

        private async void OpenAcBoSite(object obj)
        {
            await Browser.OpenAsync(Constants.ACBO_SITO, BrowserLaunchMode.SystemPreferred).ConfigureAwait(false);
        }

        private async void OpenRegistrationPage(object obj)
        {
            if(App.Current.MainPage != null && App.Current.MainPage.Navigation != null)
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new RegistrationPage()).ConfigureAwait(false);
            }
        }

        #endregion
    }
}
