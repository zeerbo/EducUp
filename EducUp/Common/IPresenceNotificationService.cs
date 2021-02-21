using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.Common
{
    public interface IPresenceNotificationService
    {
        Task PublishPresenceNotificationAsync(string messageString);
        
        Task UnpublishPresenceNotificationAsync();

        Task SubscribeMessagesAsync();

        Task UnsubscribePresenceNotificationAsync();
    }
}
