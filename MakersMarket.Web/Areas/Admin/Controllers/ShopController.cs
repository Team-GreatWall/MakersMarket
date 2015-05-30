namespace MakersMarket.Web.Areas.Admin.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using Data.Data;
    using MakersMarket.Models;
    using PagedList;
    public class ShopController : AdminController
    {
        public ShopController(IMakersMarketData data)
            : base(data)
        {
        }
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            return View(this.Data.Shops.All().OrderByDescending(category => category.Name).ToPagedList(page, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop collection = this.Data.Shops.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Shop model)
        {
            var userId = this.User.Identity.GetUserId();
            ViewData["UserId"] = userId;
            if (ModelState.IsValid)
            {
                this.Data.Shops.Add(model);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop collection = this.Data.Shops.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Description")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                this.Data.Shops.Update(shop);
                return RedirectToAction("Index");
            }
            return View(shop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "ID")] Shop shop)
        {
            this.Data.Shops.Delete(shop);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
