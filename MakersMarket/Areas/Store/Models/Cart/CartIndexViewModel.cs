using MakersMarket.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MakersMarket.Models.View.Cart
{
    public class CartIndexViewModel
    {
        [Key]
        public List<CartItem> CartItems { get; set; }

        public decimal CartTotal { get; set; }
    }
}