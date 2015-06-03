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
        public ProductOutputModel()
        {
            this.Images = new HashSet<Image>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ShopId { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}