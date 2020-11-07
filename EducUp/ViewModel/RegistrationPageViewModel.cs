using EducUp.Model;
using EducUp.Utils;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EducUp.ViewModel
{
    public class RegistrationPageViewModel : ObservableObject
    {
        #region Properties

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        #endregion


        #region Contructor

        public RegistrationPageViewModel()
        {
            User = new User();
            IsBusy = false;
        }

        #endregion


        #region Methods

        public async Task<bool> CheckIfUserExists()
        {
            bool result = await App.DataService.GetUserAsync(User.Email) != null;
            return result;
        }

        public bool CheckAdminCode(string adminCode)
        {
            return !string.IsNullOrEmpty(adminCode) && adminCode.Equals(Constants.ADMIN_CODE);
        }

        public async Task<bool> RegisterUserAsync(string password)
        {
            bool resultAuth = false;
            if (string.IsNullOrEmpty(password))
                return resultAuth;

            bool signIn = await LoginUserAsync(password);

            if (!signIn)
            {
                // Se il login non va a buon fine provo a registrare l'utente e ad eseguire nuovamente il login

                resultAuth = await App.AuthService.CreateUserWithEmailAndPassword(User.Email, password);
                resultAuth = resultAuth && await LoginUserAsync(password);
                resultAuth = resultAuth && await CreateUserAsync();
            }
            else
            {
                resultAuth = await CreateUserAsync();
            }

            if (resultAuth)
            {
                App.SaveCredentials(User.Email, password);
                App.SaveAdminProfile(User.IsAdmin);
            }

            return resultAuth;
        }

        public async Task<bool> LoginUserAsync(string password)
        {
            return await App.LoginUserAync(User.Email, password);
        }

        public async Task<bool> CreateUserAsync()
        {
            bool result = false;
            Guid presenceIdGuid = Guid.NewGuid();
            string presenceId = presenceIdGuid.ToString();
            User.PresenceId = presenceId;

            result = await App.DataService.CreateUserAsync(User).ConfigureAwait(false);
            if (result)
            {
                App.SaveUserPresenceId(presenceId);
            }

            return result;
        }

        #endregion
    }
}
