namespace MakersMarket.Web.Areas.LoggedUser.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Data.Data;
    using MakersMarket.Models;
    using PagedList;
    public class BrandController : LoggedUserController
    {
        public BrandController(IMakersMarketData data)
            : base(data)
        {
        }
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            return View(this.Data.Brands.All().OrderByDescending(brand => brand.Name).ToPagedList(page, pageSize));
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
                this.Data.Brands.Add(brand);
                this.Data.SaveChanges();
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
            Brand brand = this.Data.Brands.Find(id);
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
               this.Data.Brands.Update(brand);
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "ID")] Brand brand)
        {
            this.Data.Brands.Delete(brand);
            return RedirectToAction("Index");
        }

    }
}
