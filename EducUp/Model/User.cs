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

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
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

        #endregion


        public User() { }

        public User(string name, string surname, string username, string password, DateTime birthDate, string parish, string membership, bool isAdmin)
        {
            Name = name;
            Surname = surname;
            Username = username;
            Password = password;
            BirthDate = birthDate;
            Parish = parish;
            Membership = membership;
            IsAdmin = isAdmin;
        }
    }
}
