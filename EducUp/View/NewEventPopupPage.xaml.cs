using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        #region Handlers

        private async void CreateEventButton_Clicked(object sender, EventArgs e)
        {
            bool result = false;

            if(Vm.Event != null && !string.IsNullOrEmpty(Vm.Event.Titolo) && !string.IsNullOrEmpty(StepEntry.Text))
            {
                Vm.Event.StartDateTime.AddHours(StartTimePicker.Time.Hours);
                Vm.Event.StartDateTime.AddMinutes(StartTimePicker.Time.Minutes);
                Vm.Event.EndDateTime.AddHours(EndTimePicker.Time.Hours);
                Vm.Event.EndDateTime.AddMinutes(EndTimePicker.Time.Minutes);

                result = await Vm.CreateEventAsync();
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

        #endregion
    }
}