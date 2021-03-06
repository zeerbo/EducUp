﻿using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EducUp.ViewModel
{
    public class ProfilePageViewModel : ObservableObject
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

        private bool _modifyMode;
        public bool ModifyMode
        {
            get => _modifyMode;
            set
            {
                _modifyMode = value;
                OnPropertyChanged(nameof(ModifyMode));
            }
        }

        private bool _showAdminCodeEntry;
        public bool ShowAdminCodeEntry
        {
            get => _showAdminCodeEntry;
            set
            {
                _showAdminCodeEntry = value;
                OnPropertyChanged(nameof(ShowAdminCodeEntry));
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

        #region Constructor

        public ProfilePageViewModel() : base()
        {
            ModifyMode = false;
            ShowAdminCodeEntry = false;
            Title = "Il mio profilo";
        }

        #endregion

        #region

        public async Task<bool> SetUserAsync()
        {
            bool result = false;
            string email = App.GetUserEmail();

            if (!string.IsNullOrEmpty(email))
            {
                User = await App.DataService.GetUserAsync(email);

                if (User != null)
                {
                    SelectedDate = User.BirthDate.Date;
                    result = true;
                }
            }

            return result;
        }

        public async Task<bool> SaveUserAsync()
        {
            return await App.DataService.UpdateUserAsync(User);
        }

        #endregion
    }
}
