using EducUp.ViewModel;
using EducUp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EducUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPageMaster : ContentPage
    {
        public ListView ListView { get; private set; }

        public MasterDetailPageMaster()
        {
            InitializeComponent();

            BindingContext = new MasterDetailPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        private async void MenuItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e != null && e.Item != null && e.Item is MasterDetailPageMasterMenuItem item && item != null)
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                //var navigationPage = new NavigationPage(page);
                await Navigation.PushModalAsync(page);
            }
        }

        class MasterDetailPageMasterViewModel : ObservableObject
        {
            public ObservableCollection<MasterDetailPageMasterMenuItem> MenuItems { get; set; }

            public MasterDetailPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailPageMasterMenuItem>(new[]
                {
                    new MasterDetailPageMasterMenuItem { Title = "Profilo", TargetType = typeof(ProfilePage) }
                });
            }
        }
    }
}