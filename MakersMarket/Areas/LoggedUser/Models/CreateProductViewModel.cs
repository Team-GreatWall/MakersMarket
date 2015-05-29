using MakersMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakersMarket.Areas.LoggedUser.Models
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }

        public int? CategoryID { get; set; }

        public int? BrandID { get; set; }

        public int? ShopID { get; set; }

        public Boolean IsOnSale { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}