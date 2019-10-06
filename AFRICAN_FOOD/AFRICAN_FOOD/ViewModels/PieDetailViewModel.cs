﻿using AFRICAN_FOOD.Constants;
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
        public Xamarin.Forms.Maps.Map Map { get; private set; } = new Xamarin.Forms.Maps.Map();

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

        public async void UpdateMapNew(Pie pie)
        {
            var pin = new Xamarin.Forms.Maps.Pin {
                Type =PinType.Place,
                Position = new Position(pie.Latitude, pie.Longitude),
                Label = pie.PositionGeo,
                Address =pie.PositionGeo

            };
            Map.Pins.Add(pin);
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(0.3)));
            //Places.Add(new Place
            //{
            //    PlaceName = pie.PositionGeo,
            //    Position = new Position(pie.Latitude, pie.Longitude),

            //});
            //var loc = await Geolocation.GetLocationAsync();
            //MoveToRegion(MapSpan.FromCenterAndRadius(new Position(loc.Latitude, loc.Longitude), Distance.FromKilometers(100)));
        }

        
    }
}
