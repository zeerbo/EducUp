using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace EducUp.ViewModel.Base
{
    public class ObservableObject : INotifyPropertyChanged
    {
        private bool _isBusy;
        [Ignored]
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        [Ignored]
        public bool EnableView => !IsBusy;

        private string _title;
        [Ignored]
        public string Title 
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        
        [Ignored]
        public Command<object> BackButtonPressedCommand { get; set; }


        public ObservableObject()
        {
            BackButtonPressedCommand = new Command<object>(ClosePage);
        }

        private async void ClosePage(object obj)
        {
            var currentPage = App.Current.MainPage.Navigation.ModalStack.LastOrDefault();
            if(currentPage != null)
            {
                await currentPage.Navigation.PopModalAsync();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
