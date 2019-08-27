﻿using AFRICAN_FOOD.Constants;
using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.Extensions;
using AFRICAN_FOOD.Models;
using AFRICAN_FOOD.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AFRICAN_FOOD.ViewModels
{
    public class ShoppingCartViewModel : ViewModelBase
    {
        private ObservableCollection<ShoppingCartItem> _shoppingCartItems;
        private readonly ISettingsService _settingsService;
        private readonly IShoppingCartDataService _shoppingCartService;

        private decimal _orderTotal;
        private decimal _taxes;
        private decimal _shipping;
        private decimal _grandTotal;

        public ShoppingCartViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            IShoppingCartDataService shoppingCartService, ISettingsService settingsService) : base(connectionService, navigationService, dialogService)
        {
            _shoppingCartService = shoppingCartService;
            _settingsService = settingsService;
            _shoppingCartItems = new ObservableCollection<ShoppingCartItem>();
            _orderTotal = 0;
            //initialise();
        }

        public ICommand CheckOutCommand => new Command(OnCheckOut);

        public ObservableCollection<ShoppingCartItem> ShoppingCartItems
        {
            get => _shoppingCartItems;
            set
            {
                _shoppingCartItems = value;
                //RecalculateBasket();
                OnPropertyChanged();
            }
        }

        public decimal GrandTotal
        {
            get => _grandTotal;
            set
            {
                _grandTotal = value;
                OnPropertyChanged();
            }
        }

        public decimal Shipping
        {
            get => _shipping;
            set
            {
                _shipping = value;
                OnPropertyChanged();
            }
        }

        public decimal Taxes
        {
            get => _taxes;
            set
            {
                _taxes = value;
                OnPropertyChanged();
            }
        }

        public void InitializeMessenger()
        {
            MessagingCenter.Subscribe<PieDetailViewModel, Pie>(this, MessagingConstants.AddPieToBasket,
                (pieDetailViewModel, pie) => OnAddPieToBasketReceived(pie));
            MessagingCenter.Subscribe<HomeViewModel, Pie>(this, MessagingConstants.AddPieToBasket,
                (homeViewModel, pie) => OnAddPieToBasketReceived(pie));
            MessagingCenter.Subscribe<CheckoutViewModel>(this, "OrderPlaced", model => OnOrderPlaced());
        }

        private async void OnCheckOut()
        {
            await _dialogService.ShowDialog("Commande passée avec succès", "Succès", "OK");
            await _navigationService.PopToRootAsync();
            //_navigationService.NavigateToAsync<CheckoutViewModel>();
        }

        private void OnOrderPlaced()
        {
            ShoppingCartItems.Clear();
        }

        private void RecalculateBasket()
        {
            _orderTotal = 33;//  CalculateOrderTotal();
            Taxes = 5;//_orderTotal * (decimal)0.2;
            Shipping = _orderTotal * (decimal)0.1;
            GrandTotal = _orderTotal + _shipping + _taxes;
        }

        //private decimal CalculateOrderTotal()
        //{
        //    decimal total = 0;

        //    foreach (var shoppingCartItem in ShoppingCartItems)
        //    {
        //        total += shoppingCartItem.Quantity * shoppingCartItem.Pie.Price;
        //    }

        //    return total;
        //}

        public override async Task InitializeAsync(object data)
        {
            var shoppingCart = await _shoppingCartService.GetShoppingCart(_settingsService.UserIdSetting);
            ShoppingCartItems = shoppingCart.ShoppingCartItems.ToObservableCollection();
        }




        private async void OnAddPieToBasketReceived(Pie pie)
        {
            var shoppingCartItem = new ShoppingCartItem() { Pie = pie, PieId = pie.PieId, Quantity = 1 };

            await _shoppingCartService.AddShoppingCartItem(shoppingCartItem, _settingsService.UserIdSetting);

            ShoppingCartItems.Add(shoppingCartItem);

            //RecalculateBasket();
        }
    }
}
