using EducUp.Common;
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
    public partial class PresenceNotificationPage : ContentPage
    {
        private IPresenceNotificationService _presenceNotificationService;

        public PresenceNotificationPage()
        {
            InitializeComponent();
            if (!DesignMode.IsDesignModeEnabled)
            {
                _presenceNotificationService = DependencyService.Get<IPresenceNotificationService>(); 
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await _presenceNotificationService.UnpublishPresenceNotificationAsync();
        }

        private async void PubblishMessageButton_Clicked(object sender, EventArgs e)
        {
            PubblishMessageButton.IsVisible = false;
            await _presenceNotificationService.PublishPresenceNotificationAsync(App.GetPresenceId());
        }
    }
}