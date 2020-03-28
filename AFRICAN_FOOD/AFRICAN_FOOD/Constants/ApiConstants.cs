using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Constants
{
    public class ApiConstants
    {
        //public const string BaseApiUrl = "http://192.168.1.101:45461/";
        public static string BaseApiUrl = "http://vps730084.ovh.net/";
        public const string CatalogEndpoint = "api/catalog/pies/";
        public const string PiesOfTheWeekEndpoint = "api/catalog/piesoftheweek/";
        public const string ShoppingCartEndpoint = "api/shoppingcart";
        public const string GetCommandClient = "api/shoppingcart/GetCommandClient/";
        public const string AddShoppingCartItemEndpoint = "api/shoppingcart/";
        public const string AddContactInfoEndpoint = "api/contact";
        public const string PlaceOrderEndpoint = "api/order";
        public const string RegisterEndpoint = "api/authentication/register";
        public const string AuthenticateEndpoint = "api/authentication/authenticate";
        public const string DeleteShoppingItem = "api/shoppingcart/DeleteShoppingItem";
        public const string DeleteUser = "api/authentication/Delete";
        public const string SendMessage = "api/tchat/SendMessage";
        public const string GetAllMessage = "api/tchat/GetMessages";
        public const string GetAllAdmin = "api/tchat/GetAllAdmin";
    }

    
}