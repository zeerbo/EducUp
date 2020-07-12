using EducUp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EducUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel _vm;

        public LoginPage()
        {
            InitializeComponent();

            _vm = BindingContext as LoginPageViewModel;
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            _vm.IsBusy = true;

            if (string.IsNullOrEmpty(EmailEntry.Text))
            {
                await DisplayAlert("Attenzione", "Username errato!", "Ok");
            }
            else if (string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await DisplayAlert("Attenzione", "Username errato!", "Ok");
            }
            else
            {
                bool loginSuccess = await _vm.LoginAsync(EmailEntry.Text, PasswordEntry.Text);

                if (loginSuccess)
                {
                    App.Current.MainPage = new AppMasterDetailPage();
                }
                else
                {
                    await DisplayAlert("Attenzione", "Login non andato a buon fine!", "Ok");
                }
            }

            _vm.IsBusy = false;
        }
    }
}