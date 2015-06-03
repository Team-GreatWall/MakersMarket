using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakersMarket.Web.Areas.Store.Models.Shop
{
    using PagedList;
    using Product;

    public class ShopsPagedViewModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IPagedList<ShopOutputModel> Shops { get; set; }
    }
}