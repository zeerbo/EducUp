using EducUp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.Common
{
    public interface IDataService
    {
        #region Users

        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task<User> GetUserAsync(string email);
        Task<User> GetUserByPresenceIdAsync(string presenceId);
        Task<ObservableCollection<User>> GetUserListAsync();
        Task<ObservableCollection<User>> GetUsersListByUsernameListAsync(List<string> usernameLists);

        #endregion


        #region Events

        Task<bool> CreateEventAsync(Event evento);
        Task<bool> UpdateEventAsync(Event evento);
        Task<bool> DeleteEventAsync(Event evento);
        Task<Event> GetEventAsync(string id);
        Task<ObservableCollection<Event>> GetFutureEventListAsync();
        Task<ObservableCollection<Event>> GetNextFutureEventListPageAsync(Event evento);
        Task<ObservableCollection<Event>> GetPastEventListAsync();
        Task<ObservableCollection<Event>> GetNextPastEventListPageAsync(Event evento);
        Task<ObservableCollection<Event>> GetEventListByParticipantUsername(string username);
        Task<ObservableCollection<Event>> GetNextEventListByParticipantUsername(string username, Event evento);
        Task<List<Event>> GetUserEventByDateRange(string username, DateTime startDate, DateTime endDate);

        #endregion


        #region Participants

        Task<List<string>> GetParticipantsByEvent(string eventiId);
        Task<bool> AddParticipantByEvent(string eventiId, string userId);
        Task<bool> RemoveParticipantParticipantByEvent(string eventiId, string userId);

        #endregion
    }
}
