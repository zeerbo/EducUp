using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.Common
{
    public interface IAuthService
    {
        #region Email and Password Auth

        Task<bool> CreateUserWithEmailAndPassword(string email, string password);
        Task<bool> SignInWhitEmailAndPassword(string email, string password); 

        #endregion
    }
}
