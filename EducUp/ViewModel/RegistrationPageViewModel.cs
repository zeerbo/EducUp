using EducUp.Model;
using EducUp.Utils;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
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


        #endregion


        #region Contructor

        public RegistrationPageViewModel()
        {
            User = new User();
            IsBusy = false;
        }

        #endregion


        #region Methods

        public bool CheckAdminCode(string adminCode)
        {
            return !string.IsNullOrEmpty(adminCode) && adminCode.Equals(Constants.ADMIN_CODE);
        }

        public async Task<bool> RegisterUserAsync(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            //La password viene convertita salvata come Hash
            string hashPassword = await HashManager.GetHashStringAsync(password).ConfigureAwait(false);
            if (string.IsNullOrEmpty(hashPassword))
                return false;

            User.Password = hashPassword;

            bool result = await App.DataService.CreateUserAsync(User).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
