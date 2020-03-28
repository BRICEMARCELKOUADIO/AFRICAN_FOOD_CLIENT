using AFRICAN_FOOD.Bootstrap;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.ViewModels;
//using Com.OneSignal;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AFRICAN_FOOD
{
    public partial class App : Application
    {
        const string OneSignalKey = "353c637d-79a6-4957-8d21-6fc2225aca8c";
        public App()
        {
            InitializeComponent();

            InitializeApp();

            InitializeNavigation();
        }

        private async Task InitializeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();
            //if (!string.IsNullOrEmpty(OneSignalKey))
            //{
            //    var appContext = AppContainer.Resolve<IApplicationContext>();
            //    OneSignal.Current.StartInit(OneSignalKey)
            //        .HandleNotificationReceived(appContext.HandleNotificationReceived)
            //        .EndInit();

            //    OneSignal.Current.IdsAvailable((userID, pushToken) =>
            //    {
            //        var applicationContext = AppContainer.Resolve<IApplicationContext>();
            //        applicationContext.OneSignalUserID = userID;
            //        applicationContext.OneSignalPushToken = pushToken;
            //    });
            //}
            var shoppingCartViewModel = AppContainer.Resolve<ShoppingCartViewModel>();
            shoppingCartViewModel.InitializeMessenger();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

