using EducUp.Enumerations;
using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

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

        private TimeSpan _startTimespan;
        public TimeSpan StartTimespan
        {
            get => _startTimespan;
            set
            {
                _startTimespan = value;
                OnPropertyChanged(nameof(StartTimespan));
            }
        }

        private TimeSpan _endTimespan;
        public TimeSpan EndTimespan
        {
            get => _endTimespan;
            set
            {
                _endTimespan = value;
                OnPropertyChanged(nameof(EndTimespan));
            }
        }
        
        private DateTime _startDateTime;
        public DateTime StartDateTime
        {
            get => _startDateTime;
            set
            {
                _startDateTime = value;
                OnPropertyChanged(nameof(StartDateTime));
            }
        }

        private DateTime _endDateTime;
        public DateTime EndDateTime
        {
            get => _endDateTime;
            set
            {
                _endDateTime = value;
                OnPropertyChanged(nameof(EndDateTime));
            }
        }

        private NewEventPageMode _pageMode;
        public NewEventPageMode PageMode 
        {
            get => _pageMode;
            set
            {
                _pageMode = value;
                OnPropertyChanged(nameof(PageMode));
            } 
        }

        #endregion

        #region Methods

        public async Task<bool> CreateEventAsync(string title, string description, string location, int step)
        {
            bool result = false;
            Event.Id = App.GetNewGuid();
            
            if (!string.IsNullOrEmpty(Event.Id))
            {
                SetEventProperties(title, description, location, step);
                result = await App.DataService.CreateEventAsync(Event); 
            }

            return result;
        }

        public async Task<bool> ModifyEventAsync(string title, string description, string location, int step)
        {
            bool result = false;

            if (Event != null)
            {
                SetEventProperties(title, description, location, step);
                result = await App.DataService.UpdateEventAsync(Event);
            }

            return result;
        }

        public void InitializePropertiesModifyMode()
        {
            if (Event != null)
            {
                StartDateTime = Event.StartDateTime.Date;
                EndDateTime = Event.EndDateTime.Date;
                StartTimespan = Event.StartDateTime.TimeOfDay;
                EndTimespan = Event.EndDateTime.TimeOfDay;
            }
        }

        public void InitializePropertiesNewMode()
        {
            try
            {
                StartTimespan = new TimeSpan();
                EndTimespan = new TimeSpan();

                DateTime now = DateTime.Now;
                DateTime startEndDateTime = new DateTime(now.Year, now.Month, now.Day);
                StartDateTime = startEndDateTime;
                EndDateTime = startEndDateTime;

                Event = new Event();
            }
            catch (Exception)
            {
                Event = new Event();
            }
        }

        private void SetEventProperties(string title, string description, string location, int step)
        {
            Event.Titolo = title;
            Event.Description = description;
            Event.Location = location;
            Event.StepNumber = step;
            Event.StartDateTime = new DateTimeOffset(StartDateTime.Year, StartDateTime.Month, StartDateTime.Day, StartTimespan.Hours, StartTimespan.Minutes, 0, TimeSpan.Zero).ToUniversalTime();
            Event.EndDateTime = new DateTimeOffset(EndDateTime.Year, EndDateTime.Month, EndDateTime.Day, EndTimespan.Hours, EndTimespan.Minutes, 0, TimeSpan.Zero).ToUniversalTime();
        }

        #endregion
    }
}
