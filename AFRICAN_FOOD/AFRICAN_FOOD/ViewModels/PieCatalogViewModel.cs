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
    public class PieCatalogViewModel : ViewModelBase
    {
        private readonly ICatalogDataService _catalogDataService;

        private ObservableCollection<Pie> _pies;

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

        public PieCatalogViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            ICatalogDataService catalogDataService)
            : base(connectionService, navigationService, dialogService)
        {
            _catalogDataService = catalogDataService;
            //initialise();
        }

        public ICommand PieTappedCommand => new Command<Pie>(OnPieTapped);

        public ObservableCollection<Pie> Pies
        {
            get => _pies;
            set
            {
                _pies = value;
                OnPropertyChanged();
            }
        }

    //public void initialise()
    //    {
    //        IsBusy = true;
    //        Pies = new ObservableCollection<Pie>()
    //        {
    //            new Pie { Name = "Tarte aux pommes", Price = 12.95M, ShortDescription = "Nos fameuses tartes aux pommes!", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Fruit pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepie.jpg", InStock = false, IsPieOfTheWeek = true, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg", AllergyInformation = "" },
    //                new Pie { Name = "Gateau de fromage aux myrtilles", Price = 18.95M, ShortDescription = "Tu vas l'adorer!", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Cheese cakes"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecake.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecakesmall.jpg", AllergyInformation = "" },
    //                new Pie { Name = "Cheesecake", Price = 18.95M, ShortDescription = "Gâteau au fromage simple. Plaine plaisir.", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Cheese cakes"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg", InStock = false, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg", AllergyInformation = "" },
    //                new Pie { Name = "Tarte Aux Cerises", Price = 15.95M, ShortDescription = "Un classique de l'été!", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Fruit pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypie.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypiesmall.jpg", AllergyInformation = "" },
    //                new Pie { Name = "Tarte aux pommes de Noël", Price = 13.95M, ShortDescription = "Bon vacances avec cette tarte!", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Seasonal pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepie.jpg", InStock = false, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepiesmall.jpg", AllergyInformation = "" },
    //                new Pie { Name = "Tarte Aux Canneberges", Price = 17.95M, ShortDescription = "Un favori de Noël", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Seasonal pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypie.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypiesmall.jpg", AllergyInformation = "" },
    //                new Pie { Name = "Tarte aux pêches", Price = 15.95M, ShortDescription = "Tarte aux pêches", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Fruit pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpie.jpg", InStock = false, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpiesmall.jpg", AllergyInformation = "" },
    //                new Pie { Name = "Tarte à la citrouille", Price = 12.95M, ShortDescription = "Notre préféré Halloween", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Seasonal pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg", InStock = true, IsPieOfTheWeek = true, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg", AllergyInformation = "" },
    //                new Pie { Name = "tarte à la rhubarbe", Price = 15.95M, ShortDescription = "Mon Dieu, si gentil!", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Fruit pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg", InStock = true, IsPieOfTheWeek = true, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg", AllergyInformation = "" },
    //                new Pie { Name = "Tarte à la fraise", Price = 15.95M, ShortDescription = "Notre délicieuse tarte aux fraises!", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Fruit pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg", AllergyInformation = "" },
    //                new Pie { Name = "Gâteau Au Fromage Aux Fraises", Price = 18.95M, ShortDescription = "Tu vas l'adorer!", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Cheese cakes"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecake.jpg", InStock = false, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecakesmall.jpg", AllergyInformation = "" }

    //        };



    //        IsBusy = false;
    //    }


        private void OnPieTapped(Pie selectedPie)
        {
            _navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
        }

        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;

            Pies = (await _catalogDataService.GetAllPiesAsync()).ToObservableCollection();

            IsBusy = false;
        }
    }
}
