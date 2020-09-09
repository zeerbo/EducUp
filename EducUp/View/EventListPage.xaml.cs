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
    public partial class EventListPage : ContentPage
    {
        public EventListPage()
        {
            InitializeComponent();
        }


        #region Overrides

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Vm.SetEventList();
        }

        #endregion


        #region Handlers

        private async void AddEventButton_Clicked(object sender, EventArgs e)
        {
            NewEventPopupPage newEventPopupPage = new NewEventPopupPage();
            if(newEventPopupPage != null)
            {
                await Navigation.PushPopupAsync(newEventPopupPage);
            }
        }

        #endregion
    }
}