using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MakersMarket.Areas.Admin.Models;
using MakersMarket.Models.Domain;
using Microsoft.AspNet.Identity;
using PagedList;

namespace MakersMarket.Areas.LoggedUser.Controllers
{
    public class ProductController : LoggedUserController
    {
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            var userId = this.User.Identity.GetUserId();
            var shops = db.Shops.Where(s =>s.UserId == userId).Select(s =>s.ID);
            
            var products = db.Products.Where(p=> shops.Contains(p.Shop.ID)).OrderByDescending(product => product.Name).Include(p => p.Brand).Include(p => p.Category).Include(p =>p.Images);
      
            return View(products.ToPagedList(page, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
              var userId = this.User.Identity.GetUserId();
            ViewBag.BrandID = new SelectList(db.Brands, "ID", "Name");
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            ViewBag.ShopID = new SelectList(db.Shops.Where(s => s.UserId == userId), "ID", "Name");

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
                product.Category = db.Categories.Find(productData.CategoryID);
                product.Brand = db.Brands.Find(productData.BrandID);
                product.Price = productData.Price;
                product.ShopId = productData.ShopID;
                product.Description = productData.Description;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("AddPhoto", new { id = product.ID });
            }

            ViewBag.BrandID = new SelectList(db.Brands, "ID", "Name", productData.BrandID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", productData.CategoryID);
            return View(productData);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.Brands, "ID", "Name", product.BrandID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,CategoryID,BrandID,IsOnSale,Price,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandID = new SelectList(db.Brands, "ID", "Name", product.BrandID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "ID")] Product product)
        {
            db.Products.Attach(product);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
       // [ValidateAntiForgeryToken]
        public ActionResult AddPhoto(int id)
        {
            this.ViewBag.productId = id;
            return this.View();
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult AddPhoto(int id ,HttpPostedFileBase file)
        {
         
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    var image = new Image() { ProductId = id, ImagePath = "/Images/" + file.FileName};
                    db.Images.Add(image);
                    var product = db.Products.Where(p => p.ID == id).FirstOrDefault();
                    product.Images.Add(image);
                    db.SaveChanges();
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
            return View();
        }
    }
}
