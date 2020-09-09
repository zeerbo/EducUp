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
        Task<ObservableCollection<User>> GetUserListAsync();

        #endregion


        #region Events

        Task<bool> CreateEventAsync(Event evento);
        Task<bool> UpdateEventAsync(Event evento);
        Task<bool> DeleteEventAsync(Event evento);
        Task<Event> GetEventAsync(string id);
        Task<ObservableCollection<Event>> GetEventListAsync();
        Task<ObservableCollection<Event>> GetNextEventListPageAsync(Event evento);

        #endregion
    }
}
