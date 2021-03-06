﻿using EducUp.Model;
using EducUp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EducUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPageViewModel _vm;

        public RegistrationPage()
        {
            InitializeComponent();

            _vm = BindingContext as RegistrationPageViewModel;
        }

        private async void RegistrationButton_Clicked(object sender, EventArgs e)
        {
            _vm.IsBusy = true;

            // Controllo i dati di input
            bool checkData = CheckInputData();
            if (!checkData)
            {
                await DisplayAlert("Attenzione!", "Inserisci tutti i dati obbligatori (*) per registrarti", "Ok");
            }

            // Controllo che le due password siano uguali
            if (checkData && !PasswordEntry.Text.Equals(ConfirmPasswordEntry.Text))
            {
                checkData = false;
                await DisplayAlert("Attenzione!", "Le password non corrispondono", "Ok");
            }

            // Controllo il codice amministratore se presente
            if (checkData && AdminCheckBox.IsChecked)
            {
                checkData = _vm.CheckAdminCode(AdminCode.Text);

                if (!checkData)
                {
                    await DisplayAlert("Attenzione!", "Il codice amministratore non è corretto. Inserire il codice corretto o registrarsi come profilo normare", "Ok");
                }
            }

            _vm.User.BirthDate = new DateTimeOffset(_vm.SelectedDate.Year, _vm.SelectedDate.Month, _vm.SelectedDate.Day, 0, 0, 0, TimeSpan.Zero).ToUniversalTime();

            if (checkData)
            {
                bool userExists = await _vm.CheckIfUserExists();
                if (userExists)
                {
                    await DisplayAlert("Attenzione", "Utente già registrato", "Ok");
                }
                else
                {
                    bool registrationCompleted = await _vm.RegisterUserAsync(PasswordEntry.Text);
                    if (registrationCompleted)
                    {
                        await DisplayAlert("Complimenti!", "Registrazione completata! Esegui il login e inizia ad usare EducUp", "Ok");
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        await DisplayAlert("Spiacenti!", "La procedura non è andata a buon fine, controlla che la connessione alla rete sia attiva o riprova tra qualche minuto", "Ok");
                    }
                }
            }

            _vm.IsBusy = false;
        }

        private bool CheckInputData()
        {
            bool checkData = true;
            checkData = checkData && !string.IsNullOrEmpty(EmailEntry.Text);
            checkData = checkData && !string.IsNullOrEmpty(PasswordEntry.Text);
            checkData = checkData && !string.IsNullOrEmpty(ConfirmPasswordEntry.Text);
            checkData = checkData && !string.IsNullOrEmpty(NameEntry.Text);
            checkData = checkData && !string.IsNullOrEmpty(SurnameEntry.Text);
            checkData = checkData && !string.IsNullOrEmpty(ParishEntry.Text);
            return checkData;
        }
    }
}