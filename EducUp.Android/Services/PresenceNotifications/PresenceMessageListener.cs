using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Nearby.Messages;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace EducUp.Droid.Services.PresenceNotifications
{
    public class PresenceMessageListener : MessageListener
    {
        public override void OnFound(Message message)
        {
            string presenceId = string.Empty;

            if (message != null)
            {
                try
                {
                    presenceId = Encoding.UTF8.GetString(message.GetContent());
                    MessagingCenter.Send(this, "PRESENCE_ID_RECEIVED", presenceId);
                    Console.Out.WriteLine(presenceId);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                    Console.Error.WriteLine(e.StackTrace);
                } 
            }
        }
    }
}