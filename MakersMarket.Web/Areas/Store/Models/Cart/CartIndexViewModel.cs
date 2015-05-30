namespace MakersMarket.Web.Areas.Store.Models.Cart
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MakersMarket.Models;
    public class CartIndexViewModel
    {
        [Key]
        public List<CartItem> CartItems { get; set; }

        public decimal CartTotal { get; set; }
    }
}