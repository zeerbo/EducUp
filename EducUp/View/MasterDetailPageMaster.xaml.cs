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

        class MasterDetailPageMasterViewModel : ObservableObject
        {
            public ObservableCollection<MasterDetailPageMasterMenuItem> MenuItems { get; set; }

            public MasterDetailPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailPageMasterMenuItem>(new[]
                {
                    new MasterDetailPageMasterMenuItem { Title = "I miei passi", TargetType = typeof(StepPage) },
                    new MasterDetailPageMasterMenuItem { Title = "Eventi", TargetType = typeof(EventListPage) },
                    new MasterDetailPageMasterMenuItem { Title = "Il mio profilo", TargetType = typeof(ProfilePage) }
                });
            }
        }
    }
}