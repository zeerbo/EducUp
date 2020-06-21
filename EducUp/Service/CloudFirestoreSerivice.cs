using EducUp.Common;
using EducUp.Model;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.Service
{
    public class CloudFirestoreSerivice : IDataService
    {
        private static CloudFirestoreSerivice _instance;
        private IFirestore _firestore;

        #region Constructor

        private CloudFirestoreSerivice()
        {
            _firestore = CrossCloudFirestore.Current.Instance;
        }

        public static CloudFirestoreSerivice GetInstance()
        {
            if (_instance == null)
                _instance = new CloudFirestoreSerivice();

            return _instance;
        }

        #endregion

        #region Methods

        public async Task<bool> CreateUserAsync(User user)
        {
            bool result = false;
            if (user == null)
                return result;

            result = true;
            try
            {
                await _firestore.GetCollection(nameof(User)).GetDocument(user.Username).SetDataAsync(user).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                result = false;
                App.LogException(e);
            }

            return result;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            bool result = false;
            if (user == null)
                return result;

            result = true;
            try
            {
                await _firestore.GetCollection(nameof(User)).GetDocument(user.Username).UpdateDataAsync(user).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                result = false;
                App.LogException(e);
            }

            return result;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            bool result = false;
            if (user == null)
                return result;

            result = true;
            try
            {
                await _firestore.GetCollection(nameof(User)).GetDocument(user.Username).DeleteDocumentAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                result = false;
                App.LogException(e);
            }

            return result;
        }

        public async Task<User> GetUserAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            User result = null;
            try
            {
                IDocumentSnapshot documentSnapshot = await _firestore.GetCollection(nameof(User)).GetDocument(username).GetDocumentAsync();
                if (documentSnapshot.Exists)
                    result = documentSnapshot.ToObject<User>();
            }
            catch (Exception e)
            {
                result = null;
                App.LogException(e);
            }

            return result;
        }

        public async Task<ObservableCollection<User>> GetUserListAsync()
        {
            ObservableCollection<User> result = new ObservableCollection<User>();

            try
            {
                IQuerySnapshot querySnapshot = await _firestore.GetCollection(nameof(User)).GetDocumentsAsync();
                foreach (IDocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        User newUser = documentSnapshot.ToObject<User>();
                        result.Add(newUser);
                    }
                }
            }
            catch (Exception e)
            {
                result = null;
                App.LogException(e);
            }

            return result;
        }

        #endregion
    }
}
