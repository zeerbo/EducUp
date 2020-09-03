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

        public int _stepNumber;
        private int StepNumber
        {
            get => _stepNumber;
            set
            {
                _stepNumber = value;
                OnPropertyChanged(nameof(StepNumber));
            }
        }

        #endregion
    }
}
