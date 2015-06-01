using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakersMarket.Web.Areas.Store.Controllers
{
    using Data.Data;
    using Models.Shop;
    using Web.Controllers;

    public class ShopController : BaseController
    {
        public ShopController(IMakersMarketData data)
            : base(data)
        {
        }

        // GET: Store/Shop
        public ActionResult Index()
        {
            var shops = this.Data.Shops.All()
                .Select(s => new ShopOutputModel()
                {
                    Id = s.Id,
                    Title = s.Name,
                    Description = s.Description,
                    UserId = s.UserId
                }).ToList();

            return View(shops);
        }
    }
}