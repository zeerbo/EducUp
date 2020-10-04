using EducUp.Enumerations;
using EducUp.Model;
using EducUp.Utils;
using EducUp.ViewModel;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EducUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventListPage : ContentPage
    {
        #region Bindable Properties

        public static readonly BindableProperty PageModeProperty = BindableProperty.Create(nameof(PageMode), typeof(EventiListPageMode), typeof(EventListPage), EventiListPageMode.FutureEventMode, BindingMode.TwoWay);
        public EventiListPageMode PageMode
        {
            get => (EventiListPageMode)GetValue(PageModeProperty);
            set
            {
                SetValue(PageModeProperty, value);
            }
        }

        #endregion

        public EventListPage()
        {
            InitializeComponent();
        }


        #region Overrides

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshView();

            if (PageMode == EventiListPageMode.FutureEventMode)
            {
                MessagingCenter.Subscribe<NewEventPopupPage>(this, Constants.NEW_EVENT, async (sender) =>
                {
                    await RefreshView();
                }); 
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (PageMode == EventiListPageMode.FutureEventMode)
            {
                MessagingCenter.Unsubscribe<NewEventPopupPage>(this, Constants.NEW_EVENT); 
            }
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

        private async void LoadMoretButton_Clicked(object sender, EventArgs e)
        {
            await Vm.LoadMoreEvents(PageMode);
        }

        private async void EventListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(e != null && e.Item != null && e.Item is Event evento)
            {
                EventPage eventPage = new EventPage(evento);
                await Navigation.PushModalAsync(eventPage);
            }
        }

        #endregion


        #region Methods

        public async Task RefreshView()
        {
            Vm.SetPropertyPage(PageMode);
            await Vm.SetEventList(PageMode);
        }


        #endregion
    }
}