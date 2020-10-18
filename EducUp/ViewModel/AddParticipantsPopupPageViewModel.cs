using EducUp.Enumerations;
using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.ViewModel
{
    public class AddParticipantsPopupPageViewModel : ObservableObject
    {
        #region Properties

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

        #endregion

        #region Methods

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
                        result = resultPersistance ? AddParticipantResultEnum.Success : AddParticipantResultEnum.Fail;
                    }
                }
                else
                {
                    result = AddParticipantResultEnum.UserNotExists;
                }
            }

            return result;
        }

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
