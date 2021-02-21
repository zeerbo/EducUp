using EducUp.Common;
using EducUp.Enumerations;
using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EducUp.ViewModel
{
    public class AddParticipantsPopupPageViewModel : ObservableObject
    {
        #region Properties

        private ObservableCollection<User> _userFoundList;
        public ObservableCollection<User> UserFoundList
        {
            get => _userFoundList;
            set
            {
                _userFoundList = value;
                OnPropertyChanged(nameof(UserFoundList));
            }
        }

        private bool _activityIndicatorRunning;
        public bool ActivityIndicatorRunning
        {
            get => _activityIndicatorRunning;
            set
            {
                _activityIndicatorRunning = value;
                OnPropertyChanged(nameof(ActivityIndicatorRunning));
            }
        }

        public IPresenceNotificationService PresenceNotificationService { get; set; }

        #endregion

        public AddParticipantsPopupPageViewModel()
        {
            PresenceNotificationService = DependencyService.Get<IPresenceNotificationService>();
        }

        #region Methods

        public async Task SubscrivePresenceNotificationAsync()
        {
            MessagingCenter.Subscribe<string>(this, "PRESENCE_ID_RECEIVED", SubscribeUserFoundCallback);
            await PresenceNotificationService.SubscribeMessagesAsync();
            ActivityIndicatorRunning = true;
        }

        public async Task UnsubscribePresenceNotificationAsync()
        {
            MessagingCenter.Unsubscribe<string>(this, "PRESENCE_ID_RECEIVED");
            await PresenceNotificationService.UnsubscribePresenceNotificationAsync();
            ActivityIndicatorRunning = false;
        }

        private void SubscribeUserFoundCallback(string presenceId)
        {
            if (!string.IsNullOrEmpty(presenceId))
            {
                Task.Run(async () =>
                {
                    User user = await App.DataService.GetUserByPresenceIdAsync(presenceId);
                    if (user != null)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            UserFoundList.Add(user);
                        });
                    }
                }); 
            }
        }

        #endregion
    }
}
