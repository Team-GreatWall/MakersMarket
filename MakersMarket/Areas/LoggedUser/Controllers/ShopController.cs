using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MakersMarket.Models.Domain;
using Microsoft.AspNet.Identity;
using PagedList;

namespace MakersMarket.Areas.LoggedUser.Controllers
{
    public class ShopController : LoggedUserController
    {
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            var userId = this.User.Identity.GetUserId();
            return View(db.Shops.Where(s => s.User.Id == userId).OrderByDescending(category => category.Name).ToPagedList(page, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = db.Shops.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
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
            model.UserId = userId;
            if (ModelState.IsValid)
            {
                db.Shops.Add(model);
                db.SaveChanges();
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
            Shop shop = db.Shops.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "ID")] Shop shop)
        {
            db.Shops.Attach(shop);
            db.Shops.Remove(shop);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
