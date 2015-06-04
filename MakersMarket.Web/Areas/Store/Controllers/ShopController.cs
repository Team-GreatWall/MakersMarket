using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakersMarket.Web.Areas.Store.Controllers
{
    using System.Data.Entity;
    using System.Net;
    using Data.Data;
    using MakersMarket.Models;
    using Models.Product;
    using Models.Shop;
    using PagedList;
    using Web.Controllers;

    public class ShopController : BaseController
    {
        public ShopController(IMakersMarketData data)
            : base(data)
        {
        }

        // GET: Store/Shop
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            var shops = this.Data.Shops.All()
                .Include("ImagesShop")
                .Select(s => new ShopOutputModel()
                {
                    Id = s.Id,
                    Title = s.Name,
                    Description = s.Description,
                    UserId = s.UserId,
                    ImagePath = (s.Images.FirstOrDefault().ImagePath == null ? "~/Content/images/camera-no-image.jpg" : s.Images.FirstOrDefault().ImagePath) 
                }).ToList();

            var pagedModel = new ShopsPagedViewModel()
            {
                Page = page,
                PageSize = pageSize,
                Shops = shops.ToPagedList(page, pageSize)
            };

            return View(pagedModel);
        }

        public ActionResult Products(int? id, int page = 1, int pageSize = 12)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shop = this.Data.Shops.Find(id);

            if (shop == null)
            {
                return HttpNotFound();
            }
            var products = this.Data.Products.All()
                .Where(p => p.ShopId == id)
                .Select(p => new ProductOutputModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = (p.Images.FirstOrDefault().ImagePath == null ? "/Content/images/camera-no-image.jpg" : p.Images.FirstOrDefault().ImagePath),
                    ShopId = p.ShopId,
                    Description = p.Description
                })
                .ToList();

            var productsPaged = new ProductsPagedViewModel()
            {
                ShopId = id,
                Page = page,
                PageSize = pageSize,
                Products = products.ToPagedList(page, pageSize)
            };

            return View(productsPaged);
        }
    }
}