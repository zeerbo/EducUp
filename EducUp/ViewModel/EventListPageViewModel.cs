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

        private ILookup<DateTime, Event> _groupedEventsList;
        public ILookup<DateTime, Event> GroupedEventsList
        {
            get => _groupedEventsList;
            set
            {
                _groupedEventsList = value;
                OnPropertyChanged(nameof(GroupedEventsList));
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

        private bool _loadMoreBusy;
        public bool LoadMoreBusy
        {
            get => _loadMoreBusy;
            set
            {
                _loadMoreBusy = value;
                OnPropertyChanged(nameof(LoadMoreBusy));
            }
        }

        #endregion

        #region Methods

        public async Task SetEventList(EventiListPageMode eventiListPageMode)
        {
            switch (eventiListPageMode)
            {
                case EventiListPageMode.UserEventMode:
                    _eventsList = await App.DataService.GetEventListByParticipantUsername(App.GetUserEmail());
                    break;

                case EventiListPageMode.PastEventMode:
                    _eventsList = await App.DataService.GetPastEventListAsync();
                    break;

                case EventiListPageMode.FutureEventMode:
                default:
                    _eventsList = await App.DataService.GetFutureEventListAsync();
                    break;
            }

            if(_eventsList != null)
            {
                GroupedEventsList = _eventsList?.ToLookup(e => e.StartDateTime.Date);
            }
        }

        public async Task LoadMoreEvents(EventiListPageMode eventiListPageMode)
        {
            var lastEventLoaded = _eventsList.LastOrDefault();
            if (lastEventLoaded != null)
            {
                ObservableCollection<Event> otherEventsLoaded = null;
                switch (eventiListPageMode)
                {
                    case EventiListPageMode.UserEventMode:
                        otherEventsLoaded = await App.DataService.GetNextEventListByParticipantUsername(App.GetUserEmail(), lastEventLoaded);
                        break;

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
                    foreach(Event evento in otherEventsLoaded)
                    {
                        _eventsList.Add(evento);
                    }

                    GroupedEventsList = _eventsList.ToLookup(e => e.StartDateTime.Date);
                }
            }
        }

        public void SetPropertyPage(EventiListPageMode eventiListPageMode)
        {
            switch (eventiListPageMode)
            {
                case EventiListPageMode.UserEventMode:
                case EventiListPageMode.PastEventMode:
                    AddButtonVisible = false;
                    break;

                case EventiListPageMode.FutureEventMode:
                default:
                    AddButtonVisible = App.IsAdminProfile();
                    break;
            }
        }
        
        #endregion
    }
}
