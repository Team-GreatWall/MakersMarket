using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakersMarket.Web.Areas.Store.Models.Product
{
    using System.ComponentModel.DataAnnotations;
    using MakersMarket.Models;

    public class ProductOutputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ShopId { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}