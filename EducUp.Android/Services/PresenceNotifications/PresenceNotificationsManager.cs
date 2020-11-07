using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.Gms.Nearby;
using Android.Gms.Nearby.Messages;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EducUp.Common;
using EducUp.Droid.Services;
using EducUp.Droid.Services.PresenceNotifications;
using Xamarin.Forms;

[assembly: Dependency(typeof(PresenceNotificationsManager))]
namespace EducUp.Droid.Services.PresenceNotifications
{
    public class PresenceNotificationsManager : IPresenceNotificationService
    {
        private Message _message;
        private PresenceMessageListener _messageListener;

        public async Task<bool> PublishPresenceNotificationAsync(string messageString)
        {
            bool result = true;

            try
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(messageString);
                _message = new Message(messageBytes);
                await NearbyClass.GetMessagesClient(Android.App.Application.Context).PublishAsync(_message);
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public async Task<bool> UnPublishPresenceNotificationAsync()
        {
            bool result = true;

            if(_message != null)
            {
                try
                {
                    await NearbyClass.GetMessagesClient(Android.App.Application.Context).UnpublishAsync(_message);
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        public async Task SubscribeMessagesAsync()
        {
            SubscribeOptions.Builder builder = new SubscribeOptions.Builder();
            builder.SetStrategy(Strategy.BleOnly);
            SubscribeOptions subscribeOptions = builder.Build();

            _messageListener = new PresenceMessageListener();

            await NearbyClass.GetMessagesClient(Android.App.Application.Context).SubscribeAsync(_messageListener, subscribeOptions);
        }

        public async Task UnsubscriberMessageAsync()
        {
            if(_messageListener != null)
            {
                await NearbyClass.GetMessagesClient(Android.App.Application.Context).UnsubscribeAsync(_messageListener);
            }
        }
    }
}