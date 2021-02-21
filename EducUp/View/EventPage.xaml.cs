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
    public partial class EventPage : ContentPage
    {
        private EventPage()
        {
            InitializeComponent();
        }

        public EventPage(Event evento) : this()
        {
            Vm.Evento = evento;
            Vm.SetPropertyPage();
        }


        #region Handlers

        private async void ModifyButton_Clicked(object sender, EventArgs e)
        {
            if (Vm.Evento != null)
            {
                NewEventPopupPage newEventPopupPage = new NewEventPopupPage(Vm.Evento);
                if (newEventPopupPage != null)
                {
                    await Navigation.PushPopupAsync(newEventPopupPage);
                } 
            }
        }

        private async void ParticipantsButton_Clicked(object sender, EventArgs e)
        {
            UserListPage userListPage = new UserListPage(Vm.Evento);
            if (userListPage != null)
            {
                await Navigation.PushModalAsync(userListPage);
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool confirmDelete = await DisplayAlert("Eliminare definitivamente l'evento?", Vm.Evento?.Titolo, "Sì", "No");

            if (confirmDelete)
            {
                bool result = await Vm.DeleteEvent();
                if (result)
                {
                    await DisplayAlert("Evento eliminato con successo!", "", "Ok");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Attenzione!", "Non è stato possibile eliminare l'evento", "Ok");
                }
            }
        }

        #endregion
    }
}