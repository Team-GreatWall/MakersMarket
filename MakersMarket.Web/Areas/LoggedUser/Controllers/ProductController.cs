namespace MakersMarket.Web.Areas.LoggedUser.Controllers
{
    using System;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Models;
    using Data.Data;
    using MakersMarket.Models;
    using PagedList;
    using MakersMarket.Data;
    using System.Collections.Generic;
    public class ProductController : LoggedUserController
    {
        public ProductController(IMakersMarketData data)
            : base(data)
        {
        }
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            var userId = this.UserProfile.Id;
            var shops = this.Data.Shops.All().Where(s =>s.UserId == userId).Select(s =>s.Id);
            
            var products = this.Data.Products.All().Where(p=> shops.Contains(p.Shop.Id)).OrderByDescending(product => product.Name).Include(p => p.Brand).Include(p => p.Category).Include(p =>p.Images);
      
            return View(products.ToPagedList(page, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = this.Data.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            var userId = this.UserProfile.Id;
            ViewBag.BrandID = new SelectList(this.Data.Brands.All(), "ID", "Name");
            ViewBag.CategoryID = new SelectList(this.Data.Categories.All(), "ID", "Name");
            ViewBag.ShopID = new SelectList(this.Data.Shops.All().Where(s => s.UserId == userId), "ID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,CategoryID,ShopID,BrandID,IsOnSale,Price,Description")] CreateProductViewModel productData)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.Name = productData.Name;
//                product.Category = this.Data.Categories.Find(productData.CategoryId);
                product.CategoryId = productData.CategoryId;
//                product.Brand = this.Data.Brands.Find(productData.BrandId);
                product.BrandId = productData.BrandId;
                product.Price = productData.Price;
                product.ShopId = productData.ShopId;
                product.Description = productData.Description;

                this.Data.Products.Add(product);
                this.Data.SaveChanges();
                return RedirectToAction("AddPhoto", new { id = product.Id });
            }

            ViewBag.BrandID = new SelectList(this.Data.Brands.All(), "ID", "Name", productData.BrandId);
            ViewBag.CategoryID = new SelectList(this.Data.Categories.All(), "ID", "Name", productData.CategoryId);
            return View(productData);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = this.Data.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(this.Data.Brands.All(), "ID", "Name", product.BrandId);
            ViewBag.CategoryID = new SelectList(this.Data.Categories.All(), "ID", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,CategoryID,BrandID,IsOnSale,Price,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                this.Data.Products.Update(product);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandID = new SelectList(this.Data.Brands.All(), "ID", "Name", product.BrandId);
            ViewBag.CategoryID = new SelectList(this.Data.Categories.All(), "ID", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "ID")] Product product)
        {
            this.Data.Products.Delete(product);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
       // [ValidateAntiForgeryToken]
        public ActionResult AddPhoto(int id)
        {
            this.ViewBag.productId = id;
            var images = this.Data.Images.All().Where(i => i.ProductId == id).Select(i => i).ToList();
            return this.View(images);
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult AddPhoto(int id ,HttpPostedFileBase file)
        {
            this.ViewBag.productId = id;
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    var image = new Image() { ProductId = id, ImagePath = "/Images/" + file.FileName};
                    this.Data.Images.Add(image);
                    var product = this.Data.Products.All().FirstOrDefault(p => p.Id == id);
                    product.Images.Add(image);
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
         
            var productImages = this.Data.Images.All().Where(i => i.ProductId == id).Select(i => i).ToList();

            this.ViewBag.images = productImages;

            return View(productImages);
        }


        public ActionResult DeletePhoto(int id, int photoId)
        {
            this.Data.Images.Delete(photoId);
            this.Data.SaveChanges();
            return Redirect("/LoggedUser/Product/AddPhoto/" + id);
        }
    }
}
