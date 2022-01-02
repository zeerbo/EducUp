using EducUp.Enumerations;
using EducUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EducUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanQRCodePage : ContentPage
    {
        public ScanQRCodePage()
        {
            InitializeComponent();
        }

        public ScanQRCodePage(Event evento) : this()
        {
            Vm.Evento = evento;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Xamarin.Essentials.Permissions.Camera camera = new Xamarin.Essentials.Permissions.Camera();
            PermissionStatus status =  await camera.CheckStatusAsync();
            if (!PermissionStatus.Granted.Equals(status))
            {
                status = await camera.RequestAsync();
                if (!PermissionStatus.Granted.Equals(status))
                {
                    await DisplayAlert("Attenzione", "Senza il permesso della telecamera non è possibile usare questa funzionalità", "OK");
                    ScannerView.IsEnabled = true;
                    ScannerView.IsScanning = true;
                    await Navigation.PopModalAsync();
                }
            }
        }

        private async void ScannerView_OnScanResult(ZXing.Result result)
        {
            await Vm.AddParticipantByQrCodeAsync(result.Text);
        }

        private void ContinueButton_Clicked(object sender, EventArgs e)
        {
            Vm.ScannerVisible = true;
            Vm.MessageVisible = false;
        }
    }
}