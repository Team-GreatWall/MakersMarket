using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MakersMarket.Models.Domain;
using Microsoft.AspNet.Identity;
using PagedList;

namespace MakersMarket.Areas.Admin.Controllers
{
    public class ShopController : AdminController
    {
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            return View(db.Shops.OrderByDescending(category => category.Name).ToPagedList(page, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop collection = db.Shops.Find(id);
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
            Shop collection = db.Shops.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Description")] Shop collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "ID")] Shop collection)
        {
            db.Shops.Attach(collection);
            db.Shops.Remove(collection);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
