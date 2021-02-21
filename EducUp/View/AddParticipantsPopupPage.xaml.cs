using EducUp.Model;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EducUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddParticipantsPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public AddParticipantsPopupPage(ObservableCollection<User> foundUsers)
        {
            InitializeComponent();
            Vm.UserFoundList = foundUsers;
        }

        #region Overrides

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(500);
            await Vm.SubscrivePresenceNotificationAsync();
        }

        #endregion

        private async void AddParticipantsButton_Clicked(object sender, EventArgs e)
        {
            await Vm.UnsubscribePresenceNotificationAsync();
            await Navigation.PopPopupAsync();
        }
    }
}