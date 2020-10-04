using EducUp.Enumerations;
using EducUp.Model;
using EducUp.View;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.ViewModel
{
    public class EventListPageViewModel : ObservableObject
    {
        #region Properties

        private ObservableCollection<Event> _eventsList;
        public ObservableCollection<Event> EventsList
        {
            get => _eventsList;
            set
            {
                _eventsList = value;
                OnPropertyChanged(nameof(EventsList));
            }
        }

        private bool _addButtonVisible;
        public bool AddButtonVisible
        {
            get => _addButtonVisible;
            set
            {
                _addButtonVisible = value;
                OnPropertyChanged(nameof(AddButtonVisible));
            }
        }

        #endregion

        #region Methods

        public async Task SetEventList(EventiListPageMode eventiListPageMode)
        {
            switch (eventiListPageMode)
            {
                case EventiListPageMode.PastEventMode:
                    EventsList = await App.DataService.GetPastEventListAsync();
                    break;

                case EventiListPageMode.FutureEventMode:
                default:
                    EventsList = await App.DataService.GetFutureEventListAsync();
                    break;
            }
        }

        public async Task LoadMoreEvents(EventiListPageMode eventiListPageMode)
        {
            Event lastEventLoaded = EventsList.LastOrDefault();
            if (lastEventLoaded != null)
            {
                ObservableCollection<Event> otherEventsLoaded = null;
                switch (eventiListPageMode)
                {
                    case EventiListPageMode.PastEventMode:
                        otherEventsLoaded = await App.DataService.GetNextPastEventListPageAsync(lastEventLoaded);
                        break;

                    case EventiListPageMode.FutureEventMode:
                    default:
                        otherEventsLoaded = await App.DataService.GetNextFutureEventListPageAsync(lastEventLoaded);
                        break;
                }

                if (otherEventsLoaded != null && otherEventsLoaded.Count > 0)
                {
                    foreach (Event evento in otherEventsLoaded)
                    {
                        EventsList.Add(evento);
                    } 
                }
            }
        }

        public void SetPropertyPage(EventiListPageMode eventiListPageMode)
        {
            if (eventiListPageMode == EventiListPageMode.PastEventMode)
            {
                AddButtonVisible = false;
            }
            else
            {
                AddButtonVisible = App.IsAdminProfile();
            }
        }
        
        #endregion
    }
}
