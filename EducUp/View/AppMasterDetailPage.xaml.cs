﻿using EducUp.Utils;
using EducUp.ViewModel;
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
    public partial class AppMasterDetailPage : MasterDetailPage
    {
        public AppMasterDetailPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string username = App.GetUserEmail();
            string password = Preferences.Get(Constants.PASSWORD_PREFERENCE, string.Empty);
            if(!await App.LoginUserAync(username, password))
            {
                App.Current.MainPage = new LoginPage();
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailPageMasterMenuItem;
            if (item != null)
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(page);
                IsPresented = false;

                MasterPage.ListView.SelectedItem = null;
            }
        }
    }
}