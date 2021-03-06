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
    public partial class StepPage : ContentPage
    {
        public StepPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Vm.SetData();
        }
    }
}