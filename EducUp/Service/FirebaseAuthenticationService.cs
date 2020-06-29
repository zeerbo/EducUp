using Plugin.FirebaseAuth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EducUp.Common;
using System.Runtime.InteropServices;

namespace EducUp.Service
{
    public class FirebaseAuthenticationService : IAuthService
    {
        private static FirebaseAuthenticationService _instance;
        private IAuth _firebaseAuth;

        public FirebaseAuthenticationService()
        {
            _firebaseAuth = CrossFirebaseAuth.Current.Instance;
        }

        public static FirebaseAuthenticationService GetInstance()
        {
            if (_instance == null)
                _instance = new FirebaseAuthenticationService();

            return _instance;
        }


        #region Email and Password Auth

        public async Task<bool> CreateUserWithEmailAndPassword(string email, string password)
        {
            bool result = false;

            try
            {
                var authResult = await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password);
                if(authResult != null && authResult.User != null)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                App.LogException(e);
                result = false;
            }

            return result;
        }

        public async Task<bool> SignInWhitEmailAndPassword(string email, string password)
        {
            bool result = false;

            try
            {
                var signInResult = await _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
                if (signInResult != null && signInResult.User != null)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                App.LogException(e);
                result = false;
            }

            return result;
        }

        #endregion
    }
}
