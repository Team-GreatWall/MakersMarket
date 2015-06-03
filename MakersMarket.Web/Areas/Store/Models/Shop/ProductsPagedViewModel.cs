using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakersMarket.Web.Areas.Store.Models.Shop
{
    using MakersMarket.Models;
    using PagedList;
    using Product;

    public class ProductsPagedViewModel
    {
        public int? ShopId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IPagedList<ProductOutputModel> Products { get; set; }
    }
}