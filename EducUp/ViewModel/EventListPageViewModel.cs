using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        #endregion

        #region Methods

        public async Task SetEventList()
        {
            EventsList = await App.DataService.GetEventListAsync();
        }

        #endregion
    }
}
