using AFRICAN_FOOD.Constants;
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
            initialise();
        }

        public ICommand CheckOutCommand => new Command(OnCheckOut);

        public ObservableCollection<ShoppingCartItem> ShoppingCartItems
        {
            get => _shoppingCartItems;
            set
            {
                _shoppingCartItems = value;
                RecalculateBasket();
                OnPropertyChanged();
            }
        }

        private static Dictionary<string, Category> _categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    var categoryList = new[]
                    {
                        new Category { CategoryName = "Fruit pies" },
                        new Category { CategoryName = "Cheese cakes" },
                        new Category { CategoryName = "Seasonal pies" }
                    };

                    _categories = new Dictionary<string, Category>();

                    foreach (Category category in categoryList)
                    {
                        _categories.Add(category.CategoryName, category);
                    }
                }

                return _categories;
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

        //public void InitializeMessenger()
        //{
        //    MessagingCenter.Subscribe<PieDetailViewModel, Pie>(this, MessagingConstants.AddPieToBasket,
        //        (pieDetailViewModel, pie) => OnAddPieToBasketReceived(pie));
        //    MessagingCenter.Subscribe<HomeViewModel, Pie>(this, MessagingConstants.AddPieToBasket,
        //        (homeViewModel, pie) => OnAddPieToBasketReceived(pie));
        //    MessagingCenter.Subscribe<CheckoutViewModel>(this, "OrderPlaced", model => OnOrderPlaced());
        //}

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

        private decimal CalculateOrderTotal()
        {
            decimal total = 0;

            foreach (var shoppingCartItem in ShoppingCartItems)
            {
                total += shoppingCartItem.Quantity * shoppingCartItem.Pie.Price;
            }

            return total;
        }

        //public override async Task InitializeAsync(object data)
        //{
        //    var shoppingCart = await _shoppingCartService.GetShoppingCart(_settingsService.UserIdSetting);
        //    ShoppingCartItems = shoppingCart.ShoppingCartItems.ToObservableCollection();
        //}


        public void initialise()
        {
            _shoppingCartItems = new ObservableCollection<ShoppingCartItem>()
            {
                new ShoppingCartItem()
                {
                    Quantity =1,
                    Pie = new Pie { Name = "Tarte à la fraise", Price = 15M, ShortDescription = "Notre délicieuse tarte aux fraises!", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Fruit pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg", AllergyInformation = "" },
                    PieId = 1,
                    ShoppingCartItemId =1
                },
                new ShoppingCartItem()
                {
                    Quantity =1,
                    Pie = new Pie { Name = "Gâteau Au Fromage Aux Fraises", Price = 18M, ShortDescription = "Tu vas l'adorer!", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Cheese cakes"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecake.jpg", InStock = false, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecakesmall.jpg", AllergyInformation = "" },
                    PieId = 1,
                    ShoppingCartItemId =1
                }
            };
        }

        //private async void OnAddPieToBasketReceived(Pie pie)
        //{
        //    var shoppingCartItem = new ShoppingCartItem() { Pie = pie, PieId = pie.PieId, Quantity = 1 };

        //    await _shoppingCartService.AddShoppingCartItem(shoppingCartItem, _settingsService.UserIdSetting);

        //    ShoppingCartItems.Add(shoppingCartItem);

        //    RecalculateBasket();
        //}
    }
}
