using EducUp.Model;
using EducUp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EducUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        #region Overrides

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bool setup = await Vm.SetUserAsync();
            if (setup)
            {
                AdminCheckBox.IsChecked = Vm.User.IsAdmin;
            }
            else
            {
                await DisplayAlert("Utente non trovato!", "Verificare lo stato della connessione", "OK");
            }
        }

        #endregion


        #region Handlers

        private async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void ModifyButton_Clicked(object sender, EventArgs e)
        {
            Vm.ModifyMode = true;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Vm.IsBusy = true;

            bool result = false;
            if (!string.IsNullOrEmpty(Vm.User.Name) && !string.IsNullOrEmpty(Vm.User.Surname) && !string.IsNullOrEmpty(Vm.User.Parish))
            {
                if (!Vm.User.IsAdmin && AdminCheckBox.IsChecked && (string.IsNullOrEmpty(AdminCode.Text) || !AdminCode.Text.Equals(Constants.ADMIN_CODE)))
                {
                    await DisplayAlert("Attenzione!", "Codice amministratore non valido", "Ok");
                    return;
                }

                if (Vm.User.IsAdmin != AdminCheckBox.IsChecked)
                {
                    Vm.User.IsAdmin = AdminCheckBox.IsChecked;
                }

                Vm.User.BirthDate = new DateTimeOffset(Vm.SelectedDate.Year, Vm.SelectedDate.Month, Vm.SelectedDate.Day, 0, 0, 0, TimeSpan.Zero).ToUniversalTime();
                
                result = await Vm.SaveUserAsync();
            }
            else
            {
                await DisplayAlert("Attenzione!", "Dati non validi!", "Ok");
                return;
            }

            if (result)
            {
                await DisplayAlert("Completato!", "Dati salvati con successo", "Ok");
                Vm.ShowAdminCodeEntry = false;
                Vm.ModifyMode = false;
                App.SaveAdminProfile(Vm.User.IsAdmin);
            }
            else
            {
                await DisplayAlert("Salvataggio fallito!", "Controllare lo stato della connessione", "Ok");
            }

            Vm.IsBusy = false;
        }

        
        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            bool logout = await DisplayAlert("Attenzione!", "Vuoi davvero uscire dall'app?", "Sì", "No");

            if (logout)
            {
                Application.Current.MainPage = new LoginPage();
            }
        }



        private void AdminCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Vm.ModifyMode)
            {
                if (e.Value && !Vm.User.IsAdmin)
                {
                    Vm.ShowAdminCodeEntry = true; 
                }
                else if (!e.Value)
                {
                    Vm.ShowAdminCodeEntry = false;
                }
            }
        }

        #endregion      
    }
}