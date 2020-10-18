using EducUp.Enumerations;
using EducUp.Model;
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
    public partial class AddParticipantsPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private AddParticipantsPopupPage()
        {
            InitializeComponent();
        }

        public AddParticipantsPopupPage(Event evento) : this()
        {
            Vm.Evento = evento;
        }

        private async void ViewParticipantsButton_Clicked(object sender, EventArgs e)
        {
            UserListPage userListPage = new UserListPage(Vm.Evento);
            if (userListPage != null)
            {
                IsVisible = false;
                await Navigation.PushModalAsync(userListPage); 
                await Navigation.PopPopupAsync();
            }
        }

        private async void AddParticipantButton_Clicked(object sender, EventArgs e)
        {
            string username = await DisplayPromptAsync("Inserisci il nome utente", "Digita lo username dell'utente e premi aggiungi", "Aggiungi", "Annulla", "Username");
            AddParticipantResultEnum result = await Vm.AddParticipant(username);
            ManageResultAddParticipant(result);
        }

        private void AddGroupParticipantsButton_Clicked(object sender, EventArgs e)
        {

        }

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