using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakersMarket.Web.Areas.Store.Models.Shop
{
    public class ShopOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string UserId { get; set; }

        public string Image { get; set; }
    }
}