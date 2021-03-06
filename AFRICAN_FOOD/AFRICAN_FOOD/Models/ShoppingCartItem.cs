﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }

        public Pie Pie { get; set; }

        public int PieId { get; set; }
        public string ClientNumber { get; set; }

        public int Quantity { get; set; }

        public string ClientId { get; set; }
        public string ClientName { get; set; }

        public decimal Total => Quantity * Pie.PrixPromotionnel;
    }
}
