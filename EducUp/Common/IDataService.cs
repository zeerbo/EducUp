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
        #region User

        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task<User> GetUserAsync(string email);
        Task<ObservableCollection<User>> GetUserListAsync();

        #endregion
    }
}
