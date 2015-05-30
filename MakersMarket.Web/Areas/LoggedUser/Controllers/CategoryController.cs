namespace MakersMarket.Web.Areas.LoggedUser.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Data.Data;
    using MakersMarket.Models;
    using PagedList;
    public class CategoryController : LoggedUserController
    {
        public CategoryController(IMakersMarketData data)
            : base(data)
        {
        }
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            return View(this.Data.Categories.All().OrderByDescending(category => category.Name).ToPagedList(page, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = this.Data.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                this.Data.Categories.Add(category);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = this.Data.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                this.Data.Categories.Update(category);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "ID")] Category category)
        {
            this.Data.Categories.Delete(category);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
