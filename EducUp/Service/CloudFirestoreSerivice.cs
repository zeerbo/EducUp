using EducUp.Common;
using EducUp.Model;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
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

        #region Users

        public async Task<bool> CreateUserAsync(User user)
        {
            bool result = false;
            
            if (user != null)
            {
                result = true;

                try
                {
                    await _firestore.GetCollection(nameof(User)).GetDocument(user.Email).SetDataAsync(user).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    result = false;
                    App.LogException(e);
                }
            }

            return result;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            bool result = false;
            
            if (user != null)
            {
                result = true;

                try
                {
                    await _firestore.GetCollection(nameof(User)).GetDocument(user.Email).UpdateDataAsync(user).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    result = false;
                    App.LogException(e);
                }
            }

            return result;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            bool result = false;
            
            if (user != null)
            {
                result = true;

                try
                {
                    await _firestore.GetCollection(nameof(User)).GetDocument(user.Email).DeleteDocumentAsync().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    result = false;
                    App.LogException(e);
                }
            }
            
            return result;
        }

        public async Task<User> GetUserAsync(string email)
        {
            User result = null;

            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    IDocumentSnapshot documentSnapshot = await _firestore.GetCollection(nameof(User)).GetDocument(email).GetDocumentAsync();
                    if (documentSnapshot.Exists)
                    {
                        result = documentSnapshot.ToObject<User>();
                    }
                }
                catch (Exception e)
                {
                    result = null;
                    App.LogException(e);
                }
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


        #region Events

        public async Task<bool> CreateEventAsync(Event evento)
        {
            bool result = false;

            if(evento != null && !string.IsNullOrEmpty(evento.Id))
            {
                result = true;

                try
                {
                    await _firestore.GetCollection(nameof(Event)).GetDocument(evento.Id).SetDataAsync(evento).ConfigureAwait(false);
                }
                catch(Exception e)
                {
                    result = false;
                    App.LogException(e);
                }
            }

            return result;
        }

        public async Task<bool> UpdateEventAsync(Event evento)
        {
            bool result = false;

            if (evento != null && !string.IsNullOrEmpty(evento.Id))
            {
                result = true;

                try
                {
                    await _firestore.GetCollection(nameof(Event)).GetDocument(evento.Id).UpdateDataAsync(evento).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    result = false;
                    App.LogException(e);
                }
            }

            return result;
        }

        public async Task<bool> DeleteEventAsync(Event evento)
        {
            bool result = false;

            if (evento != null && !string.IsNullOrEmpty(evento.Id))
            {
                result = true;

                try
                {
                    await _firestore.GetCollection(nameof(Event)).GetDocument(evento.Id).DeleteDocumentAsync().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    result = false;
                    App.LogException(e);
                }
            }

            return result;
        }

        public async Task<Event> GetEventAsync(string id)
        {
            Event result = null;

            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    IDocumentSnapshot documentSnapshot = await _firestore.GetCollection(nameof(Event)).GetDocument(id).GetDocumentAsync();
                    if (documentSnapshot.Exists)
                    {
                        result = documentSnapshot.ToObject<Event>();
                    }
                }
                catch (Exception e)
                {
                    result = null;
                    App.LogException(e);
                }
            }

            return result;
        }

        public async Task<ObservableCollection<Event>> GetEventListAsync()
        {
            ObservableCollection<Event> result = new ObservableCollection<Event>();

            try
            {
                IQuerySnapshot querySnapshot = await _firestore.GetCollection(nameof(Event)).OrderBy(nameof(Event.StartDateTime)).LimitTo(20).GetDocumentsAsync();
                foreach (IDocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Event newEvent = documentSnapshot.ToObject<Event>();
                        result.Add(newEvent);
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

        public async Task<ObservableCollection<Event>> GetNextEventListPageAsync(Event evento)
        {
            ObservableCollection<Event> result = new ObservableCollection<Event>();

            if (evento != null)
            {
                try
                {
                    IQuerySnapshot querySnapshot = await _firestore.GetCollection(nameof(Event)).OrderBy(nameof(Event.StartDateTime)).StartAfter(evento).LimitTo(20).GetDocumentsAsync();
                    foreach (IDocumentSnapshot documentSnapshot in querySnapshot.Documents)
                    {
                        if (documentSnapshot.Exists)
                        {
                            Event newEvent = documentSnapshot.ToObject<Event>();
                            result.Add(newEvent);
                        }
                    }
                }
                catch (Exception e)
                {
                    result = null;
                    App.LogException(e);
                }

            }
            return result;
        }

        #endregion
    }
}
