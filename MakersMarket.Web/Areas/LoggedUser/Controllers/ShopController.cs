namespace MakersMarket.Web.Areas.LoggedUser.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Data.Data;
    using MakersMarket.Models;
    using Models;
    using PagedList;
    public class ShopController : LoggedUserController
    {
        public ShopController(IMakersMarketData data)
            : base(data)
        {
        }
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            var userId = this.UserProfile.Id;
            var shops = this.Data.Shops.All()
                    .Where(s => s.User.Id == userId)
                    .OrderByDescending(category => category.Name)
                    .ToPagedList(page, pageSize);
            return View(shops);
        }

        public ActionResult Create()
        {
            var model = new ShopInputViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopInputViewModel model)
        {
            var userId = this.UserProfile.Id;
            ViewData["UserId"] = userId;
            model.UserId = userId;
            if (ModelState.IsValid)
            {
                Shop newShop = new Shop()
                {
                    UserId = userId,
                    Name = model.Name,
                    Description = model.Description,
                };
                this.Data.Shops.Add(newShop);
                this.Data.SaveChanges();
                this.AddPhoto(newShop.Id, model.ImageFile);
                ViewBag.Message = "Shop created successfully.";

                return this.RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = this.Data.Shops.Find(id);

            if (shop == null)
            {
                return HttpNotFound();
            }

            var model = new ShopInputViewModel()
            {
                UserId = shop.UserId,
                ShopId = shop.Id,
                Name = shop.Name,
                Description = shop.Description,
                ImagePath = shop.Images.FirstOrDefault().ImagePath
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShopInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var shopUpdate = new Shop()
                {
                    Id = model.ShopId,
                    Description = model.Description,
                    Name = model.Name,
                    UserId = model.UserId
                };
                this.AddPhoto(model.ShopId, model.ImageFile);

                this.Data.Shops.Update(shopUpdate);
                this.Data.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "ID")] Shop shop)
        {
            this.Data.Shops.Delete(shop);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
//        [ValidateAntiForgeryToken]
        public ActionResult AddPhoto(int id)
        {
            this.ViewBag.productId = id;
            return this.View();
        }

    
        [NonAction]
        public void AddPhoto(int id, HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    var image = new ImageShop() { ShopId = id, ImagePath = "/Images/" + file.FileName };

                    var existingImage = this.Data.ImagesShop.All().FirstOrDefault(i => i.ShopId == id);
                    if (existingImage != null)
                    {
                        existingImage.ImagePath = image.ImagePath;
                        this.Data.SaveChanges();
                        ViewBag.Message = "File updated successfully";
                        return;
                    }

                    this.Data.ImagesShop.Add(image);
                    var shop = this.Data.Shops.All().FirstOrDefault(s => s.Id == id);
                    shop.Images.Add(image);
                    this.Data.SaveChanges();
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
        }
    }
}
