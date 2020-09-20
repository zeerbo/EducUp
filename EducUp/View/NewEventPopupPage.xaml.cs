using EducUp.Enumerations;
using EducUp.Model;
using EducUp.Utils;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EducUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEventPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public NewEventPopupPage()
        {
            InitializeComponent();
        }

        public NewEventPopupPage(Event evento) : this()
        {
            Vm.Event = evento;
            Vm.PageMode = NewEventPageMode.ModifyEvent;
        }

        #region Overrides 

        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitializeViewModel();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ManageRefreshBackView();
        }

        #endregion


        #region Handlers

        private async void CreateEventButton_Clicked(object sender, EventArgs e)
        {
            bool result = false;

            if(Vm.Event != null && !string.IsNullOrEmpty(TitleEntry.Text) && !string.IsNullOrEmpty(StepEntry.Text))
            {
                try
                {
                    int step = Convert.ToInt32(StepEntry.Text);
                    result = await Vm.CreateEventAsync(TitleEntry.Text, DescriptionEditor.Text, LocationEntry.Text, step);
                    if (result)
                    {
                        await DisplayAlert("Completato!", "Evento creato con successo", "Ok");
                        await Navigation.PopPopupAsync();
                    }
                    else
                    {
                        await DisplayAlert("Attenzione!", "Non è stato possibile creare l'evento", "Ok");
                    }
                }
                catch
                {
                    await DisplayAlert("Attenzione!", "Non è stato possibile creare l'evento", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Attenzione!", "Dati obbligatori mancanti", "Ok");
            }
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            NormalizeData();
        }

        private void StartTimePicker_Unfocused(object sender, FocusEventArgs e)
        {
            NormailzeTime();
        }

        private void EndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            NormalizeData();
        }

        private void EndTimePicker_Unfocused(object sender, FocusEventArgs e)
        {
            NormailzeTime();
        }

        private async void SwipeDown_Swiped(object sender, SwipedEventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
        private async void ModifyEventButton_Clicked(object sender, EventArgs e)
        {
            bool result = false;

            if (Vm.Event != null && !string.IsNullOrEmpty(Vm.Event.Titolo) && !string.IsNullOrEmpty(StepEntry.Text))
            {
                try
                {
                    int step = Convert.ToInt32(StepEntry.Text);
                    result = await Vm.ModifyEventAsync(TitleEntry.Text, DescriptionEditor.Text, LocationEntry.Text, step);
                    if (result)
                    {
                        await DisplayAlert("Completato!", "Evento modificato con successo", "Ok");
                        await Navigation.PopPopupAsync();
                    }
                    else
                    {
                        await DisplayAlert("Attenzione!", "Non è stato possibile salvare le modifiche", "Ok");
                    }
                }
                catch
                {
                    await DisplayAlert("Attenzione!", "Non è stato possibile salvare le modifiche", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Attenzione!", "Dati obbligatori mancanti", "Ok");
            }
        }

        #endregion


        #region Methods

        private void NormalizeData()
        {
            if (EndDatePicker.Date < StartDatePicker.Date)
            {
                EndDatePicker.Date = StartDatePicker.Date;
            }
        }

        private void NormailzeTime()
        {
            if (EndTimePicker.Time <= StartTimePicker.Time)
            {
                TimeSpan timeSpanToAdd = StartTimePicker.Time < TimeSpan.FromHours(23) ? TimeSpan.FromHours(1) : TimeSpan.FromMinutes(59);
                EndTimePicker.Time = StartTimePicker.Time + timeSpanToAdd;
            }
        }

        private void ManageRefreshBackView()
        {
            MessagingCenter.Send(this, Constants.NEW_EVENT);
        }

        private void InitializeViewModel()
        {
            switch (Vm.PageMode)
            {
                case NewEventPageMode.NewEvent:
                    Vm.InitializePropertiesNewMode();
                    break;

                case NewEventPageMode.ModifyEvent:
                    Vm.InitializePropertiesModifyMode();
                    break;

                default:
                    break;
            }
        }

        #endregion
    }
}