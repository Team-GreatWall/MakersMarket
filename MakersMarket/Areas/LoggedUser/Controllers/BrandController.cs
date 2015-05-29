using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MakersMarket.Models.Domain;
using PagedList;

namespace MakersMarket.Areas.LoggedUser.Controllers
{
    public class BrandController : LoggedUserController
    {
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            return View(db.Brands.OrderByDescending(brand => brand.Name).ToPagedList(page, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Description")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Description")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "ID")] Brand brand)
        {
            db.Brands.Attach(brand);
            db.Brands.Remove(brand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
