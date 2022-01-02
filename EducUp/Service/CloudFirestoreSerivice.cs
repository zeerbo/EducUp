using EducUp.Common;
using EducUp.Model;
using EducUp.Utils;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
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
                    await _firestore.Collection(nameof(User)).Document(user.Email).SetAsync<User>(user).ConfigureAwait(false);
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
                    await _firestore.Collection(nameof(User)).Document(user.Email).UpdateAsync<User>(user).ConfigureAwait(false);
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
                    await _firestore.Collection(nameof(User)).Document(user.Email).DeleteAsync().ConfigureAwait(false);
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
                    IDocumentSnapshot documentSnapshot = await _firestore.Collection(nameof(User)).Document(email).GetAsync();
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

        public async Task<User> GetUserByPresenceIdAsync(string presenceId)
        {
            User result = null;

            if (!string.IsNullOrEmpty(presenceId))
            {
                try
                {
                    IQuerySnapshot querySnapshot = await _firestore.Collection(nameof(User)).WhereEqualsTo(nameof(User.PresenceId), presenceId).GetAsync();
                    IDocumentSnapshot documentSnapshot = querySnapshot.Documents.FirstOrDefault();
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
                IQuerySnapshot querySnapshot = await _firestore.Collection(nameof(User)).GetAsync();
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

        public async Task<ObservableCollection<User>> GetUsersListByUsernameListAsync(List<string> usernameLists)
        {
            ObservableCollection<User> result = new ObservableCollection<User>();

            if (usernameLists != null && usernameLists.Count > 0)
            {
                try
                {
                    IQuerySnapshot querySnapshot = await _firestore.Collection(nameof(User)).GetAsync();
                    foreach (IDocumentSnapshot documentSnapshot in querySnapshot.Documents)
                    {
                        if (documentSnapshot.Exists)
                        {
                            User newUser = documentSnapshot.ToObject<User>();
                            if (usernameLists.Contains(newUser.Email))
                            {
                                result.Add(newUser); 
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    result = new ObservableCollection<User>();
                    App.LogException(e);
                } 
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
                    await _firestore.Collection(nameof(Event)).Document(evento.Id).SetAsync(evento).ConfigureAwait(false);
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
                    await _firestore.Collection(nameof(Event)).Document(evento.Id).UpdateAsync(evento).ConfigureAwait(false);
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
                    await _firestore.Collection(nameof(Event)).Document(evento.Id).DeleteAsync().ConfigureAwait(false);
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
                    IDocumentSnapshot documentSnapshot = await _firestore.Collection(nameof(Event)).Document(id).GetAsync();
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

        public async Task<ObservableCollection<Event>> GetFutureEventListAsync()
        {
            ObservableCollection<Event> result = new ObservableCollection<Event>();

            try
            {
                IQuerySnapshot querySnapshot = await _firestore.Collection(nameof(Event))
                                                                .WhereGreaterThanOrEqualsTo(nameof(Event.StartDateTime), DateTime.Now)
                                                                .OrderBy(nameof(Event.StartDateTime))
                                                                .LimitTo(20)
                                                                .GetAsync();
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

        public async Task<ObservableCollection<Event>> GetNextFutureEventListPageAsync(Event evento)
        {
            ObservableCollection<Event> result = new ObservableCollection<Event>();

            if (evento != null)
            {
                try
                {
                    IDocumentSnapshot documentSnapshotLast = await _firestore.Collection(nameof(Event)).Document(evento.Id).GetAsync();
                    if (documentSnapshotLast != null)
                    {
                        IQuerySnapshot querySnapshot = await _firestore.Collection(nameof(Event))
                                                                        .WhereGreaterThanOrEqualsTo(nameof(Event.StartDateTime), DateTime.Now)
                                                                        .OrderBy(nameof(Event.StartDateTime))
                                                                        .StartAfter(documentSnapshotLast)
                                                                        .LimitTo(20)
                                                                        .GetAsync();
                        foreach (IDocumentSnapshot documentSnapshot in querySnapshot.Documents)
                        {
                            if (documentSnapshot.Exists)
                            {
                                Event newEvent = documentSnapshot.ToObject<Event>();
                                result.Add(newEvent);
                            }
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

        public async Task<ObservableCollection<Event>> GetPastEventListAsync()
        {
            ObservableCollection<Event> result = new ObservableCollection<Event>();

            try
            {
                IQuerySnapshot querySnapshot = await _firestore.Collection(nameof(Event))
                                                                .WhereLessThan(nameof(Event.StartDateTime), DateTime.Now)
                                                                .OrderBy(nameof(Event.StartDateTime), true)
                                                                .LimitTo(20)
                                                                .GetAsync();
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

        public async Task<ObservableCollection<Event>> GetNextPastEventListPageAsync(Event evento)
        {
            ObservableCollection<Event> result = new ObservableCollection<Event>();

            try
            {
                IDocumentSnapshot documentSnapshotLast = await _firestore.Collection(nameof(Event)).Document(evento.Id).GetAsync();
                if (documentSnapshotLast != null)
                {
                    IQuerySnapshot querySnapshot = await _firestore.Collection(nameof(Event))
                                                                            .WhereLessThan(nameof(Event.StartDateTime), DateTime.Now)
                                                                            .OrderBy(nameof(Event.StartDateTime), true)
                                                                            .StartAfter(documentSnapshotLast)
                                                                            .LimitTo(20)
                                                                            .GetAsync();
                    foreach (IDocumentSnapshot documentSnapshot in querySnapshot.Documents)
                    {
                        if (documentSnapshot.Exists)
                        {
                            Event newEvent = documentSnapshot.ToObject<Event>();
                            result.Add(newEvent);
                        }
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

        public async Task<ObservableCollection<Event>> GetEventListByParticipantUsername(string username)
        {
            ObservableCollection<Event> result = new ObservableCollection<Event>();

            if (!string.IsNullOrEmpty(username))
            {
                try
                {
                    IQuerySnapshot querySnapshot = await _firestore.Collection(nameof(Event))
                                                                            .WhereArrayContains(nameof(Event.UsersList), username)
                                                                            .OrderBy(nameof(Event.StartDateTime), true)
                                                                            .LimitTo(20)
                                                                            .GetAsync();
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

        public async Task<ObservableCollection<Event>> GetNextEventListByParticipantUsername(string username, Event evento)
        {
            ObservableCollection<Event> result = new ObservableCollection<Event>();

            try
            {
                IDocumentSnapshot documentSnapshotLast = await _firestore.Collection(nameof(Event)).Document(evento.Id).GetAsync();
                if (documentSnapshotLast != null)
                {
                    IQuerySnapshot querySnapshot = await _firestore.Collection(nameof(Event))
                                                                           .WhereArrayContains(nameof(Event.UsersList), username)
                                                                           .OrderBy(nameof(Event.StartDateTime), true)
                                                                           .StartAfter(documentSnapshotLast)
                                                                           .LimitTo(20)
                                                                           .GetAsync();
                    foreach (IDocumentSnapshot documentSnapshot in querySnapshot.Documents)
                    {
                        if (documentSnapshot.Exists)
                        {
                            Event newEvent = documentSnapshot.ToObject<Event>();
                            result.Add(newEvent);
                        }
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

        public async Task<List<Event>> GetUserEventByDateRange(string username, DateTime startDate, DateTime endDate)
        {
            List<Event> result = new List<Event>();

            if (!string.IsNullOrEmpty(username))
            {
                try
                {
                    IQuerySnapshot querySnapshot = await _firestore.Collection(nameof(Event))
                                                                            .WhereArrayContains(nameof(Event.UsersList), username)
                                                                            .WhereGreaterThanOrEqualsTo(nameof(Event.StartDateTime), startDate)
                                                                            .WhereLessThanOrEqualsTo(nameof(Event.StartDateTime), endDate)
                                                                            .GetAsync();
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


        #region Participants

        public async Task<List<string>> GetParticipantsByEvent(string eventiId)
        {
            List<string> result = new List<string>();

            try
            {
                IQuerySnapshot querySnapshot = await _firestore.Collection(nameof(Event)).Document(eventiId).Collection(Constants.ENTITY_PARTICIPANTS).GetAsync();
                foreach (IDocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        result.Add(documentSnapshot.ToString());
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

        public async Task<bool> AddParticipantByEvent(string eventiId, string userId)
        {
            bool result = false;
            try
            {
                IDocumentReference documentReference = await _firestore.Collection(nameof(Event)).Document(eventiId).Collection(Constants.ENTITY_PARTICIPANTS).AddAsync<string>(userId);
                if(documentReference != null)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                result = false;
                App.LogException(e);
            }
            return result;
        }

        public async Task<bool> RemoveParticipantParticipantByEvent(string eventiId, string userId)
        {
            bool result = false;
            try
            {
                await _firestore.Collection(nameof(Event)).Document(eventiId).Collection(Constants.ENTITY_PARTICIPANTS).Document(userId).DeleteAsync();
                result = true;
            }
            catch (Exception e)
            {
                result = false;
                App.LogException(e);
            }
            return result;
        }

        #endregion
    }
}
