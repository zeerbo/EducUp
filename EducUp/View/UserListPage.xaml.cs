using EducUp.Enumerations;
using EducUp.Model;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EducUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserListPage : ContentPage
    {
        public UserListPage()
        {
            InitializeComponent();
        }

        public UserListPage(Event evento) : this()
        {
            Vm.Evento = evento;
        }


        #region Overrides

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Vm.SetDataAsync();
            await Vm.ManageAddGroupParticipants();
        }

        protected override bool OnBackButtonPressed()
        {
            bool result = true;
            if (Vm.ModifyMode)
            {
                Vm.ResumeModify();
            }
            else
            {
                result = base.OnBackButtonPressed(); 
            }

            return result;
        }


        #endregion


        #region Handlers

        private async void SaveUserListButton_Clicked(object sender, EventArgs e)
        {
            await Vm.SaveModify();
            Vm.ModifyMode = false;
        }

        private void ModifyButton_Clicked(object sender, EventArgs e)
        {
            Vm.ModifyMode = true;
        }

        private async void AddGroupParticipantsButton_Clicked(object sender, EventArgs e)
        {
            Vm.UsersToAdd = new ObservableCollection<User>();
            await Navigation.PushPopupAsync(new AddParticipantsPopupPage(Vm.UsersToAdd));
        }

        private async void AddParticipantButton_Clicked(object sender, EventArgs e)
        {
            string username = await DisplayPromptAsync("Inserisci il nome utente", "Digita lo username dell'utente e premi aggiungi", "Aggiungi", "Annulla", "Username");
            AddParticipantResultEnum result = await Vm.AddParticipant(username);
            ManageResultAddParticipant(result);
        }

        #endregion


        #region Methods

        private async void ManageResultAddParticipant(AddParticipantResultEnum result)
        {
            switch (result)
            {
                case AddParticipantResultEnum.Success:
                    await DisplayAlert("Utente aggiunto!", "", "Ok");
                    break;

                case AddParticipantResultEnum.UserAlreadyAdded:
                    await DisplayAlert("Attenzione!", "L'utente è già presente tra i partecipanti", "Ok");
                    break;

                case AddParticipantResultEnum.UserNotExists:
                    await DisplayAlert("Attenzione!", "L'utente non esiste", "Ok");
                    break;

                case AddParticipantResultEnum.Fail:
                default:
                    await DisplayAlert("Attenzione!", "Inserimento fallito, riprovare tra qualche minuto", "Ok");
                    break;
            }
        }

        #endregion
    }
}