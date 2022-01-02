using EducUp.Enumerations;
using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Linq;
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
                OnPropertyChanged(nameof(SaveButtonVisible));
                OnPropertyChanged(nameof(ModifyFrameVisible));
            }
        }

        public bool SaveButtonVisible => ModifyMode;
        
        public bool ModifyFrameVisible => !ModifyMode;

        public ObservableCollection<User> UsersToAdd { get; set; }

        public Command<object> DeleteUserCommand { get; set; }

        #endregion

        #region Constructor

        public UserListPageViewModel()
        {
            Title = "Lista Partecipanti";
            _deletedUsersList = new List<User>();
            DeleteUserCommand = new Command<object>(DeleteUserFromList);
            UsersToAdd = new ObservableCollection<User>();
        }

        #endregion

        #region Methods

        public async Task SetDataAsync()
        {
            if(Evento != null && UsersList == null)
            {
                List<string> participants = Evento.UsersList;
                if (participants != null && participants.Count > 0)
                {
                    UsersList = await App.DataService.GetUsersListByUsernameListAsync(participants); 
                }
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

        public async Task<AddParticipantResultEnum> AddParticipant(string username)
        {
            AddParticipantResultEnum result = AddParticipantResultEnum.Fail;

            if (!string.IsNullOrEmpty(username))
            {
                username = username.Trim();
                User user = await App.DataService.GetUserAsync(username);

                if (user != null)
                {
                    result = AddParticipantToEventUserList(username);
                    if (result == AddParticipantResultEnum.Success)
                    {
                        bool resultPersistance = await App.DataService.UpdateEventAsync(Evento);
                        if (resultPersistance)
                        {
                            UsersList.Add(user);
                            Evento.UsersList.Add(user.Email);
                            result = AddParticipantResultEnum.Success; 
                        }
                        else
                        {
                            result = AddParticipantResultEnum.Fail;
                        }
                    }
                }
                else
                {
                    result = AddParticipantResultEnum.UserNotExists;
                }
            }

            return result;
        }

        public async Task ManageAddGroupParticipants()
        {
            if(UsersToAdd != null && UsersToAdd.Count > 0)
            {
                if(UsersList == null)
                {
                    UsersList = new ObservableCollection<User>();
                }

                foreach (User user in UsersToAdd)
                {
                    if (!UsersList.Any(u => u.Email.Equals(user.Email)))
                    {
                        AddParticipantResultEnum result = AddParticipantToEventUserList(user.Email);
                        if (result == AddParticipantResultEnum.Success)
                        {
                            UsersList.Add(user);
                        }
                    }
                }

                await App.DataService.UpdateEventAsync(Evento);
            }
        }

        #endregion


        #region Private Methods

        private AddParticipantResultEnum AddParticipantToEventUserList(string username)
        {
            AddParticipantResultEnum result = AddParticipantResultEnum.Fail;

            if (Evento.UsersList == null)
            {
                Evento.UsersList = new List<string>();
            }

            if (!Evento.UsersList.Contains(username))
            {
                Evento.UsersList.Add(username);
                result = AddParticipantResultEnum.Success;
            }
            else
            {
                result = AddParticipantResultEnum.UserAlreadyAdded;
            }

            return result;
        }

        #endregion
    }
}
