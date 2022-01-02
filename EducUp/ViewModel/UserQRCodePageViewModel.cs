using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducUp.ViewModel
{
    public class UserQRCodePageViewModel : ObservableObject
    {
        private string _userPresenceId;
        public string UserPresenceId 
        { 
            get => _userPresenceId;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _userPresenceId = "Uncknown";
                }
                else
                {
                    _userPresenceId = value;
                }

                OnPropertyChanged(nameof(UserPresenceId));
            } 
        }
    }
}
