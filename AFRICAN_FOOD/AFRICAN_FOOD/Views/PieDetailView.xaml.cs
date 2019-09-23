using AFRICAN_FOOD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AFRICAN_FOOD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PieDetailView : ContentPage
    {
        public PieDetailView()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Bootstrap.AppContainer.Resolve<PieDetailViewModel>().ViewModelMap = MyMap;
            
        }
    }
}