using AFRICAN_FOOD.Constants;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.Models;
using AFRICAN_FOOD.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AFRICAN_FOOD.ViewModels
{
    public class PieDetailViewModel : ViewModelBase
    {
        private Pie _selectedPie;
        private ObservableCollection<Place> _places = new ObservableCollection<Place>();
        public Xamarin.Forms.Maps.Map ViewModelMap { get; set; }

        public PieDetailViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService)
            : base(connectionService, navigationService, dialogService)
        { }

        public ICommand AddToCartCommand => new Command(OnAddToCart);


        public ObservableCollection<Place> Places
        {
            get { return _places; }
            set
            {
                _places = value;
                OnPropertyChanged();
            }
        }

        public Pie SelectedPie
        {
            get => _selectedPie;
            set
            {
                _selectedPie = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object pie)
        {
            SelectedPie = (Pie)pie;
            await Task.Delay(2000);
            UpdateMapNew(SelectedPie);
            //UpdateMap(SelectedPie)
        }

        private async void OnAddToCart()
        {
            MessagingCenter.Send(this, MessagingConstants.AddPieToBasket, SelectedPie);
            await _dialogService.ShowDialog("Produit ajouté à votre panier", "Success", "OK");
        }

        //private void OnReadDescription()
        //{
        //    DependencyService.Get<ITextToSpeech>().ReadText(SelectedPie.ShortDescription);
        //}

        //public class MapTest
        //{
        //    public async Task NavigateToBuilding25()
        //    {
        //        var location = new Location(47.645160, -122.1306032);
        //        var options = new MapLaunchOptions { Name = "Microsoft Building 25" };



        //        await Map.OpenAsync(location, options);
        //    }
        //}


        public  void UpdateMapNew(Pie pie)
        {
            var pin = new Xamarin.Forms.Maps.Pin {
                Type =PinType.Place,
                Position = new Position(pie.Latitude, pie.Longitude),
                Label = pie.PositionGeo,
                Address =pie.PositionGeo

            };
            ViewModelMap.Pins.Add(pin);
            ViewModelMap.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(0.3)));
            //Places.Add(new Place
            //{
            //    PlaceName = pie.PositionGeo,
            //    Position = new Position(pie.Latitude, pie.Longitude),
                
            //});
            //var loc = await Geolocation.GetLocationAsync();
                   // MoveToRegion(MapSpan.FromCenterAndRadius(new Position(loc.Latitude, loc.Longitude), Distance.FromKilometers(100)));
        }

        //private async void UpdateMap(Pie pie)
        //{
        //    try
        //    {

        //        //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
        //        //Stream stream = assembly.GetManifestResourceStream("MapApp.Places.json");
        //        //string text = string.Empty;
        //        //using (var reader = new StreamReader(stream))
        //        //{
        //        //    text = reader.ReadToEnd();
        //        //}

        //        //var resultObject = JsonConvert.DeserializeObject<Places>(text);

        //        foreach (var place in resultObject.results)
        //        {
        //            placesList.Add(new Place
        //            {
        //                PlaceName = place.name,
        //                Address = place.vicinity,
        //                Location = place.geometry.location,
        //                Position = new Position(place.geometry.location.lat, place.geometry.location.lng),
        //                //Icon = place.icon,
        //                //Distance = $"{GetDistance(lat1, lon1, place.geometry.location.lat, place.geometry.location.lng, DistanceUnit.Kiliometers).ToString("N2")}km",
        //                //OpenNow = GetOpenHours(place?.opening_hours?.open_now)
        //            });
        //        }

        //        MyMap.ItemsSource = placesList;
        //        //PlacesListView.ItemsSource = placesList;
        //        //var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();
        //        MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(47.6370891183, -122.123736172), Distance.FromKilometers(100)));

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }


        //}
    }
}
