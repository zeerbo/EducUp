using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EducUp.ViewModel
{
    public class UserListPageViewModel : ObservableObject
    {
        #region Properties

        private List<User> _deletedUsersList;

        private Event _evento;
        public Event Evento
        {
            get => _evento;
            set
            {
                _evento = value;
                OnPropertyChanged(nameof(Evento));
            }
        }

        private ObservableCollection<User> _usersList;
        public ObservableCollection<User> UsersList
        {
            get => _usersList;
            set
            {
                _usersList = value;
                OnPropertyChanged(nameof(UsersList));
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

        public Command<object> DeleteUserCommand { get; set; }

        #endregion

        #region Constructor

        public UserListPageViewModel()
        {
            Title = "Lista Partecipanti";
            _deletedUsersList = new List<User>();
            DeleteUserCommand = new Command<object>(DeleteUserFromList);
        }

        #endregion

        #region Methods

        public async Task SetDataAsync()
        {
            if(Evento != null)
            {
                UsersList = await App.DataService.GetUsersListByUsernameListAsync(Evento.UsersList);
            }
        }
        
        public async Task SaveModify()
        {
            Evento.UsersList = new List<string>();

            foreach (User user in UsersList)
            {
                Evento.UsersList.Add(user.Email);
            }

            await App.DataService.UpdateEventAsync(Evento);

            _deletedUsersList.Clear();
        }

        public void ResumeModify()
        {
            if (_deletedUsersList != null)
            {
                foreach(User user in _deletedUsersList)
                {
                    if (!UsersList.Contains(user))
                    {
                        UsersList.Add(user);
                    }
                }

                ModifyMode = false;
            }
        }

        private void DeleteUserFromList(object obj)
        {
            if(obj != null && obj is User user && UsersList.Contains(user))
            {
                UsersList.Remove(user);
                _deletedUsersList.Add(user);
            }
        }

        #endregion
    }
}
