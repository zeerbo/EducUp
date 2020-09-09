using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.ViewModel
{
    public class NewEventPopupPageViewModel : ObservableObject 
    {
        #region Properties

        private Event _event;
        public Event Event
        {
            get => _event;
            set
            {
                _event = value;
                OnPropertyChanged(nameof(Event));
            }
        }

        #endregion

        public NewEventPopupPageViewModel()
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime startEndDateTime = new DateTime(now.Year, now.Month, now.Day);
                Event = new Event
                {
                    StartDateTime = startEndDateTime,
                    EndDateTime = startEndDateTime
                };
            }
            catch (Exception)
            {
                Event = new Event();
            }

        }

        #region Methods

        public async Task<bool> CreateEventAsync()
        {
            bool result = false;
            Event.Id = App.GetNewGuid();
            
            if (!string.IsNullOrEmpty(Event.Id))
            {
                result = await App.DataService.CreateEventAsync(Event); 
            }

            return result;
        }

        #endregion
    }
}
