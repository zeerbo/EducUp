using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EducUp.Model
{
    public class User : ObservableObject
    {
        #region Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged(nameof(Surname));
                }
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        private DateTimeOffset _birthDate;
        public DateTimeOffset BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    OnPropertyChanged(nameof(BirthDate));
                }
            }
        }

        private string _parish;
        public string Parish
        {
            get { return _parish; }
            set
            {
                if (_parish != value)
                {
                    _parish = value;
                    OnPropertyChanged(nameof(Parish));
                }
            }
        }

        private string _membership;
        public string Membership
        {
            get { return _membership; }
            set
            {
                if (_membership != value)
                {
                    _membership = value;
                    OnPropertyChanged(nameof(Membership));
                }
            }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }

        public string PresenceId { get; set; }

        #endregion


        public User() 
        {
            BirthDate = DateTime.Now;
        }

        public User(string name, string surname, string email, DateTime birthDate, string parish, string membership, bool isAdmin)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
            Parish = parish;
            Membership = membership;
            IsAdmin = isAdmin;
        }
    }
}
