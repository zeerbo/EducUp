using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.View
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

        #endregion
    }
}
