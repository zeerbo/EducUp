using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Text;
using Xamarin.Essentials;

namespace EducUp.Model
{
    public class Event : ObservableObject
    {
        #region Properties

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _titolo;
        public string Titolo
        {
            get => _titolo;
            set
            {
                _titolo = value;
                OnPropertyChanged(nameof(Titolo));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
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

        private int _stepNumber;
        public int StepNumber
        {
            get => _stepNumber;
            set
            {
                _stepNumber = value;
                OnPropertyChanged(nameof(StepNumber));
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        
        #endregion
    }
}
