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
using Xamarin.Forms.PlatformConfiguration;

[assembly: Dependency(typeof(PresenceNotificationsManager))]
namespace EducUp.Droid.Services.PresenceNotifications
{
    public class PresenceNotificationsManager : IPresenceNotificationService
    {
        private Android.Gms.Nearby.Messages.Message _message;
        private MessageListener _messageListener;

        public async Task PublishPresenceNotificationAsync(string messageString)
        {
            //Intent intent = new Intent(Android.App.Application.Context, typeof(PresenceNotificationSenderActivity));
            //intent.PutExtra("presenceId", messageString);
            //intent.SetFlags(ActivityFlags.NewTask);
            //Android.App.Application.Context.StartActivity(intent);string messageString = Intent.GetStringExtra("presenceId");
            byte[] messageByte = Encoding.UTF8.GetBytes(messageString);
            _message = new Android.Gms.Nearby.Messages.Message(messageByte);
            await NearbyClass.GetMessagesClient(MainActivity.CurrentMainActivity).PublishAsync(_message);
        }

        public async Task UnpublishPresenceNotificationAsync()
        {
            await NearbyClass.GetMessagesClient(MainActivity.CurrentMainActivity).UnpublishAsync(_message);
        }

        public async Task SubscribeMessagesAsync()
        {
            //Intent intent = new Intent(Android.App.Application.Context, typeof(PresenceNotificationReceiverActivity));
            //intent.SetFlags(ActivityFlags.NewTask);
            //Android.App.Application.Context.StartActivity(intent);
            _messageListener = new PresenceMessageListener();
            await NearbyClass.GetMessagesClient(MainActivity.CurrentMainActivity).SubscribeAsync(_messageListener);
        }

        public async Task UnsubscribePresenceNotificationAsync()
        {
            await NearbyClass.GetMessagesClient(MainActivity.CurrentMainActivity).UnsubscribeAsync(_messageListener);
        }
    }
}