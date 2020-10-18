using EducUp.Model;
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
    public partial class UserListPage : ContentPage
    {
        public UserListPage()
        {
            InitializeComponent();
        }

        public UserListPage(Event evento) : this()
        {
            Vm.Evento = evento;
        }

        #region Overrides

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Vm.SetDataAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            bool result = true;
            if (Vm.ModifyMode)
            {
                Vm.ResumeModify();
            }
            else
            {
                result = base.OnBackButtonPressed(); 
            }

            return result;
        }


        #endregion

        #region Handlers

        private async void ModifyUsersListButton_Clicked(object sender, EventArgs e)
        {
            if (!Vm.ModifyMode)
            {
                Vm.ModifyMode = true; 
            }
            else
            {
                await Vm.SaveModify();
                Vm.ModifyMode = false;
            }
        }

        #endregion

    }
}