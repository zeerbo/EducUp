using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducUp.ViewModel
{
    public class StepPageViewModel : ObservableObject
    {
        #region Properties

        private int _totalSteps;
        public int TotalSteps 
        { 
            get => _totalSteps;
            set
            {
                _totalSteps = value;
                OnPropertyChanged(nameof(TotalSteps));
            } 
        }

        private int _currentSteps;
        public int CurrentSteps
        {
            get => _currentSteps;
            set
            {
                _currentSteps = value;
                OnPropertyChanged(nameof(CurrentSteps));
            }
        }

        private string _stepsText;
        public string StepsText
        {
            get => _stepsText;
            set
            {
                _stepsText = value;
                OnPropertyChanged(nameof(StepsText));
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        #endregion

        #region Public Methods

        public async Task SetData()
        {
            DateTime startDate;
            DateTime endDate;
            if(DateTime.Now.Month >= 9)
            {
                startDate = new DateTime(DateTime.Now.Year, 9, 1);
                endDate = new DateTime(DateTime.Now.Year + 1, 8, 1);
            }
            else
            {
                startDate = new DateTime(DateTime.Now.Year - 1, 9, 1);
                endDate = new DateTime(DateTime.Now.Year, 8, 1);
            }

            List<Event> events = await App.DataService.GetUserEventByDateRange(App.GetUserEmail(), startDate, endDate);
            if(events != null)
            {
                try
                {
                    CurrentSteps = events.Select(e => e.StepNumber).Sum();
                }
                catch(Exception e)
                {
                    CurrentSteps = 0;
                }
            }
            else
            {
                CurrentSteps = 0;
            }

            TotalSteps = 60;
            StepsText = string.Format("{0}/{1}", CurrentSteps, TotalSteps);

            int difference = TotalSteps - CurrentSteps;
            if (difference <= 0)
            {
                Message = "Complimeti! Hai completato tutti i passi!";
            }
            else if (difference < 10)
            {
                Message = "Ti mancano ancora pochi passi!";
            }
            else
            {
                Message = "Continua a partecipare agli eventi proposti per procedere con i passi";
            }
        }

        #endregion
    }
}
