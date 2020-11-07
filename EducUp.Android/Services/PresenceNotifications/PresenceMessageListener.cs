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

namespace EducUp.Droid.Services.PresenceNotifications
{
    public class PresenceMessageListener : MessageListener
    {
        public override void OnFound(Message message)
        {
            string messageString = string.Empty;

            if (message != null)
            {
                try
                {
                    messageString = Encoding.UTF8.GetString(message.GetContent());
                    Console.Out.WriteLine(messageString);
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