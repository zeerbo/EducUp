using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.Common
{
    public interface IPresenceNotificationService
    {
        Task<bool> PublishPresenceNotificationAsync(string messageString);
        
        Task<bool> UnPublishPresenceNotificationAsync();

        Task SubscribeMessagesAsync();

        Task UnsubscriberMessageAsync();
    }
}
