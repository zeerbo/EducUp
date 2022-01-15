using EducUp.Enumerations;
using EducUp.Model;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EducUp.ViewModel
{
    public class ScanQRCodePageViewModel : ObservableObject
    {
        #region Properties

        private Event _evento;
        public Event Evento 
        {
            get => _evento;
            set
            {
                _evento = value;
                OnPropertyChanged(nameof(Evento));
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private bool _messageVisible;
        public bool MessageVisible
        {
            get => _messageVisible;
            set
            {
                _messageVisible = value;
                OnPropertyChanged(nameof(MessageVisible));
            }
        }

        private bool _scannerVisible;
        public bool ScannerVisible
        {
            get => _scannerVisible;
            set
            {
                _scannerVisible = value;
                OnPropertyChanged(nameof(ScannerVisible));
            }
        }

        private Color _messageColor;
        public Color MessageColor
        {
            get => _messageColor;
            set
            {
                _messageColor = value;
                OnPropertyChanged(nameof(MessageColor));
            }
        }

        #endregion

        #region Constructor

        public ScanQRCodePageViewModel()
        {
            Title = "Scanner";
            ScannerVisible = true;
            MessageVisible = false;
            Message = string.Empty;
            MessageColor = Color.White;
        }

        #endregion


        #region Methods

        public async Task AddParticipantByQrCodeAsync(string presenceId)
        {
            Message = "Inserimento fallito";
            MessageColor = Color.Red;
            if (!string.IsNullOrEmpty(presenceId))
            {
                if(Evento.UsersList == null)
                {
                    Evento.UsersList = new List<string>();
                }

                User user = await App.DataService.GetUserByPresenceIdAsync(presenceId);
                if (user != null)
                {
                    if (!Evento.UsersList.Contains(user.Email))
                    {
                        Evento.UsersList.Add(user.Email);
                        bool resultUpdate = await App.DataService.UpdateEventAsync(Evento);
                        if (resultUpdate)
                        {
                            Message = $"Utente {user.Name} {user.Surname} è stato aggiunto all'evento";
                            MessageColor = Color.Green;
                        }
                    }
                    else
                    {
                        Message = $"Utente {user.Name} {user.Surname} partecipa già all'evento";
                        MessageColor = Color.Green;
                    }
                }
                else
                {
                    Message = $"QRCode non valido";
                }
            }
            ScannerVisible = false;
            MessageVisible = true;
        }

        #endregion
    }
}
