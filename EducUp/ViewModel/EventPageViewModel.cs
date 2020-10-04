using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.ViewModel
{
    public class EventPageViewModel : ObservableObject
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

        private bool _isManagmentPanelVisible;
        public bool IsManagmentPanelVisible
        {
            get => _isManagmentPanelVisible;
            set
            {
                _isManagmentPanelVisible = value;
                OnPropertyChanged(nameof(IsManagmentPanelVisible));
            }
        }

        private bool _isModifyButtonEnabled;
        public bool IsModifyButtonEnabled
        {
            get => _isModifyButtonEnabled;
            set
            {
                _isModifyButtonEnabled = value;
                OnPropertyChanged(nameof(IsModifyButtonEnabled));
            }
        }

        private bool _isAddParticipantsButtonEnabled;
        public bool IsAddParticipantsButtonEnabled
        {
            get => _isAddParticipantsButtonEnabled;
            set
            {
                _isAddParticipantsButtonEnabled = value;
                OnPropertyChanged(nameof(IsAddParticipantsButtonEnabled));
            }
        }

        private bool _isDeleteButtonEnabled;
        public bool IsDeleteButtonEnabled
        {
            get => _isDeleteButtonEnabled;
            set
            {
                _isDeleteButtonEnabled = value;
                OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            }
        }

        #endregion


        #region Constructor

        public EventPageViewModel()
        {
            Title = "Evento";
        }

        #endregion


        #region Methods

        public async Task<bool> DeleteEvent()
        {
            bool result = false;

            if(Evento != null)
            {
                result = await App.DataService.DeleteEventAsync(Evento);
            }

            return result;
        }

        public void SetPropertyPage()
        {
            bool isAdminProfile = App.IsAdminProfile();

            if (isAdminProfile)
            {
                IsManagmentPanelVisible = true;
                IsModifyButtonEnabled = true;
                IsDeleteButtonEnabled = true;
                IsAddParticipantsButtonEnabled = true;
            }
            else
            {
                IsManagmentPanelVisible = false;
                IsModifyButtonEnabled = false;
                IsDeleteButtonEnabled = false;
                IsAddParticipantsButtonEnabled = false;
            }
        }

        #endregion
    }
}
