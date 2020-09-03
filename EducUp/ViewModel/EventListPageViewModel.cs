using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
    }
}
